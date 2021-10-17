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
            int i = 0;
            while (PlayerGone[i] == true)
            {
                i = Random.Range(0, 3);
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
        BatEngine.BatRun = true;
    }
    public static void NextLevel()
    {
        Debug.Log("all players have gone");
    }
}
