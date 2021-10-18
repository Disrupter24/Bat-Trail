using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    private static Manager _instance;
    [System.NonSerialized] public static int[] FruitScore = new int[4];
    public static int PlayerTurn = 0;
    public static int MapNumber = 0;
    public static bool[] PlayerGone = new bool[4];
    public static bool[] PlayerDidStash = new bool[4];
    public static bool[] PlayerDidDie = new bool[4];
    public static int[] StashCount = new int[4];
    public static int SharedFruitTotal;

    public static Manager Instance
    {
        get
        {
            if (_instance == null)
            {
                Manager singleton = GameObject.FindObjectOfType<Manager>();
                if (singleton == null)
                {
                    GameObject go = new GameObject();
                    _instance = go.AddComponent<Manager>();
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public static void NextPlayerCheck()
    {
        if (!DoneCheck())
        {
            int counter = 0;
            int i = 0;
            while (PlayerGone[i] == true && counter < 1000)
            {
                i = Random.Range(0, 4);
            }
            PlayerTurn = i;
            NextPlayer();
        }
        else
        {
            PlayerTurn = Random.Range(0, 3);
            NextLevel();
        }
    }
    public static bool DoneCheck()
    {
        for (int i = 0; i < PlayerGone.Length; i++)
        {
            if (PlayerGone[i] == false)
            {
                return false;
            }  
        }
        return true;
    }
    public static void NextPlayer()
    {
        string skinstring = "sprites/Bat" + PlayerTurn.ToString();
        GameObject.Find("Bat/BatSprite").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(skinstring);
        GameObject.Find("Bat").transform.position = GenerationSettings.StartPoint;
        Debug.Log(Manager.PlayerTurn + " is up to bat!");
        BatEngine.BatRun = true;
        GameObject.Find("Bat").GetComponent<BatEngine>().FruitCounter.text = "0";
        GameObject.Find("Bat").GetComponent<BatEngine>().StepCounter.text = BatEngine.StepLimit.ToString();
    }
    public static void NextLevel()
    {
        Debug.Log("All players have gone!");
        Debug.Log(SharedFruitTotal);
        Debug.Log(StashCount[0]);
        Debug.Log(PlayerDidStash[0]);
        Debug.Log(StashCount[1]);
        Debug.Log(PlayerDidStash[1]);
        Debug.Log(StashCount[2]);
        Debug.Log(PlayerDidStash[2]);
        Debug.Log(StashCount[3]);
        Debug.Log(PlayerDidStash[3]);
    }
}
