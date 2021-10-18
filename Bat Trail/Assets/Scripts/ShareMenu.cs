using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShareMenu : MonoBehaviour
{
    public Text StashText;
    private int StashAmount;
    public Text ShareText;
    private int ShareAmount;
    private int FruitTotal;
   // public Text RequirementText;
    public Text TotalText;
    public GameObject ToStashButton;
    public GameObject ToShareButton;
    public Sprite OnSprite;
    public Sprite OffSprite;
    public GameObject NonZeroMenu;
    public void Cycle()
    {
        ToStashButton.GetComponent<Image>().sprite = OffSprite;
        ToShareButton.GetComponent<Image>().sprite = OnSprite;
        FruitTotal = Manager.FruitScore[Manager.PlayerTurn];
        if (FruitTotal == 0)
        {
            NonZeroMenu.SetActive(false);
        }
        else
        {
            NonZeroMenu.SetActive(true);
        }
        StashAmount = FruitTotal;
        ShareAmount = 0;
        UpdateText();
    }

    public void ToStash()
    {
        Debug.Log("Clicked ToStash");
        if (ShareAmount != 0)
        {
            if (StashAmount == 0)
            {
                ToShareButton.GetComponent<Image>().sprite = OnSprite;
            }
            StashAmount++;
            ShareAmount--;
            if (ShareAmount == 0)
            {
                ToStashButton.GetComponent<Image>().sprite = OffSprite;
            }
        }
        UpdateText();
    }
    public void ToShare()
    {
        Debug.Log("Clicked ToShare");
        if (StashAmount != 0)
        {
            if (ShareAmount == 0)
            {
                ToStashButton.GetComponent<Image>().sprite = OnSprite;
            }
            ShareAmount++;
            StashAmount--;
            if (StashAmount == 0)
            {
                ToShareButton.GetComponent<Image>().sprite = OffSprite;
            }
        }
        UpdateText();
    }
    public void Submit()
    {
        if (StashAmount != 0)
        {
            Manager.PlayerDidStash[Manager.PlayerTurn] = true;
        }
        else
        {
            Manager.PlayerDidStash[Manager.PlayerTurn] = false;
        }
        Manager.StashCount[Manager.PlayerTurn] = Manager.StashCount[Manager.PlayerTurn] + StashAmount;
        Manager.SharedFruitTotal += ShareAmount;
        Manager.PlayerGone[Manager.PlayerTurn] = true;
        Manager.NextPlayerCheck();
        gameObject.SetActive(false);
    }
    public void UpdateText()
    {
        TotalText.text = FruitTotal.ToString();
        ShareText.text = ShareAmount.ToString();
        StashText.text = (StashAmount + Manager.StashCount[Manager.PlayerTurn]).ToString();
    //    RequirementText.text = GenerationSettings.Requirements[Manager.MapNumber].ToString();
    }
}
