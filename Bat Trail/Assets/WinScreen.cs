using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScreen : MonoBehaviour
{
    public GameObject[] Texts = new GameObject[5];
    private bool AllDead;
    public void Awake()
    {
        AllDead = true;
        for (int i = 0; i < Manager.PlayerDidDie.Length; i++)
        {
            if (!Manager.PlayerDidDie[i])
            {
                AllDead = false;
                Texts[i].SetActive(true);
            }
        }
        if (AllDead)
        {
            Texts[4].SetActive(true);
        }
    }
}
