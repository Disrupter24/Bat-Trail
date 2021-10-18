using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VoteMenu : MonoBehaviour
{
    public static bool CallingOut;
    public static int CalledOutPlayer;
    public static int CallingOutPlayer;
    public GameObject[] Buttons = new GameObject[4];
    public SpriteRenderer[] Sprites = new SpriteRenderer[4];
    public GameObject WhoCallsText;
    public static bool NoPack = false;
    public void LoadMenu()
    {
        if (NoPack)
        {
            ConfirmButton();
        }
        WhoCallsText.GetComponent<Text>().text = ("Each player needs " + (GenerationSettings.Requirements[Manager.MapNumber]).ToString() + " fruit(s) to survive.");
        CallingOut = true;
        int incount = 4;
        for (int i = 0; i < Sprites.Length; i++)
        {
            Buttons[i].transform.GetChild(0).GetComponent<Text>().text = ("Call Player " + (i + 1).ToString());
            if (Manager.PlayerLeftPack[i])
            {
                Sprites[i].color = Color.gray;
                Buttons[i].SetActive(false);
                Buttons[i].transform.GetChild(0).GetComponent<Text>().enabled = false;
            }
            else
            {
                Sprites[i].color = Color.white;
                Buttons[i].SetActive(true);
                Buttons[i].transform.GetChild(0).GetComponent<Text>().enabled = true;
            }
            if (Manager.PlayerDidDie[i])
            {
                Sprites[i].color = Color.black;
                Buttons[i].SetActive(false);
                Buttons[i].transform.GetChild(0).GetComponent<Text>().text = "R.I.P.";
            }
            if(Manager.PlayerDidDie[i] | Manager.PlayerLeftPack[i])
            {
                incount--;
            }
            if (incount < 2)
            {
                Debug.Log("The Pack has dissolved");
                NoPack = true;
                ConfirmButton();
            }
        }
    }
    public void Button0()
    {
        if (CallingOut)
        {
            CalledOutPlayer = 0;
            WhoCalls();
        }
        else
        {
            CallingOutPlayer = 0;
            CalledOut();
        }
    }
    public void Button1()
    {
        if (CallingOut)
        {
            CalledOutPlayer = 1;
            WhoCalls();
        }
        else
        {
            CallingOutPlayer = 1;
            CalledOut();
        }
    }
    public void Button2()
    {
        if (CallingOut)
        {
            CalledOutPlayer = 2;
            WhoCalls();
        }
        else
        {
            CallingOutPlayer = 2;
            CalledOut();
        }
    }
    public void Button3()
    {
        if (CallingOut)
        {
            CalledOutPlayer = 3;
            WhoCalls();
        }
        else
        {
            CallingOutPlayer = 3;
            CalledOut();
        }
    }
    public void ConfirmButton()
    {
        gameObject.SetActive(false);
        Manager.DeadCheck();
    }
    public void WhoCalls()
    {
        CallingOut = false;
        Buttons[CalledOutPlayer].SetActive(false);
        WhoCallsText.GetComponent<Text>().text = ("Who is calling out Player " + (CalledOutPlayer+1).ToString() + "?");
        for (int i = 0; i < Buttons.Length; i++)
        {
            if (i != CalledOutPlayer)
            {
                Buttons[i].transform.GetChild(0).GetComponent<Text>().text = ("Player " + (i + 1).ToString());
            }
        }
    }
    public void CalledOut()
    {
        if (Manager.PlayerDidStash[CalledOutPlayer])
        {
            Manager.PlayerLeftPack[CalledOutPlayer] = true;
        }
        else
        {
            Manager.PlayerLeftPack[CallingOutPlayer] = true;
        }
        LoadMenu();
    }
    
}
