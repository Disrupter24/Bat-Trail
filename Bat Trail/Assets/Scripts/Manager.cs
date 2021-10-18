using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    private static Manager _instance;
    [System.NonSerialized] public static int[] FruitScore = new int[4];
    public static int PlayerTurn = 0;
    public static int MapNumber = 0;
    public static bool[] PlayerGone = new bool[4];
    public static bool[] PlayerDidStash = new bool[4];
    public static bool[] PlayerDidDie = new bool[4];
    public static bool[] PlayerLeftPack = new bool[4];
    public static int[] StashCount = new int[4];
    public static int[] SharedLastRound = new int[4];
    public static int SharedFruitTotal;
    private static GameObject Bat;

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
            while (PlayerGone[i] == true | PlayerDidDie[i] | PlayerLeftPack[i])
            {
                i = Random.Range(0, 4);
            }
            PlayerTurn = i;
            NextPlayer();
        }
        else if (!PackDoneCheck())
        {
            PackDone();
            int i = 0;
            while (PlayerGone[i] == true | PlayerDidDie[i])
            {
                i = Random.Range(0, 4);
            }
            PlayerTurn = i;
            NextPlayer();
        }
        else
        {
            PlayerTurn = Random.Range(0, 3);
            if (!VoteMenu.NoPack) 
            {
                VoteTime();
            }
            else
            {
                DeadCheck();
            }
        }
    }
    public static void PackDone()
    {
        Debug.Log("All non-pack players have gone");
    }
    public static bool DoneCheck()
    {
        for (int i = 0; i < PlayerGone.Length; i++)
        {
            if (PlayerGone[i] == false && PlayerDidDie[i] == false && PlayerLeftPack[i] == false)
            {
                return false;
            }  
        }
        return true;
    }
    public static bool PackDoneCheck()
    {
        for (int i = 0; i < PlayerGone.Length; i++)
        {
            if (PlayerGone[i] == false && PlayerDidDie[i] == false)
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
        Debug.Log("Loading Skin: " + skinstring);
        if (Bat == null)
        {
            Bat = GameObject.Find("Bat");
        }
        Bat.transform.position = GenerationSettings.StartPoint;
        Debug.Log("Player " + (PlayerTurn+1) + " is up to bat!");
        FruitScore[PlayerTurn] = 0;
        BatEngine.BatRun = true;
        Bat.GetComponent<BatEngine>().FruitCounter.text = "0";
        MapPreparation.FlagsRemaining = BatEngine.FlagMax;
        Bat.GetComponent<BatEngine>().StepCounter.text = BatEngine.StepLimit.ToString();
        Bat.GetComponent<BatEngine>().FlagCounter.text = MapPreparation.FlagsRemaining.ToString();
    }
    public static void VoteTime()
    {
        Bat.GetComponent<BatEngine>().VoteMenu.SetActive(true);
        Bat.GetComponent<BatEngine>().VoteTextUpdate();
        Bat.GetComponent<BatEngine>().VoteMenu.GetComponent<VoteMenu>().LoadMenu();
    }
    public static void NextLevel()
    {
        for (int i = 0; i < PlayerGone.Length; i++)
        {
            PlayerGone[i] = false;
            SharedLastRound[i] = 0;
        }
        if (MapNumber < 5)
        {
            MapNumber++;
            SceneManager.LoadScene(1);
            GenerationSettings.LoadMap();
            NextPlayer();
        }
        else
        {
            EndGame();
        }        
    }
    public static void DeadCheck()
    {
        if (Mathf.FloorToInt(SharedFruitTotal / 4) < GenerationSettings.Requirements[MapNumber])
        {
            for (int i = 0; i < PlayerDidDie.Length; i++)
            {
                if (!PlayerLeftPack[i])
                {
                    if (Mathf.FloorToInt(SharedFruitTotal / 4) + StashCount[i] < GenerationSettings.Requirements[MapNumber])
                    {
                        PlayerDidDie[i] = true;
                        Debug.Log("Player " + (i + 1) + " is dead.");
                    }
                    else
                    {
                        StashCount[i] -= (GenerationSettings.Requirements[MapNumber] - Mathf.FloorToInt(SharedFruitTotal / 4));
                    }
                }
                else
                {
                    if (StashCount[i] < GenerationSettings.Requirements[MapNumber])
                    {
                        PlayerDidDie[i] = true;
                        Debug.Log("Player " + (i + 1) + " is dead.");
                    }
                    else
                    {
                        StashCount[i] -= GenerationSettings.Requirements[MapNumber];
                    }
                }
                
            }
        }
        int count = 0;
        for (int i = 0; i < PlayerDidDie.Length; i++)
        {
            if (PlayerDidDie[i])
            {
                count++;
            }
        }
        if (count > 2)
        {
            EndGame();
            return;
        }
        NextLevel();
    }
    public static void EndGame()
    {
        Debug.Log("The Game is Over");
        Debug.Log("Is Player 1 dead?");
        Debug.Log(PlayerDidDie[0]);
        Debug.Log("Is Player 2 dead?");
        Debug.Log(PlayerDidDie[1]);
        Debug.Log("Is Player 3 dead?");
        Debug.Log(PlayerDidDie[2]);
        Debug.Log("Is Player 4 dead?");
        Debug.Log(PlayerDidDie[3]);
        GameObject.Find("Bat").GetComponent<BatEngine>().WinScreen.SetActive(true);
    }
}
