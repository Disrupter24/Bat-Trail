using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public GameObject GameOver;
    public GameObject LevelComplete;
    public GameObject PLayAgainButton;
    public GameObject CloseButton;
    public Text CollectingPhaseText;
    public GameObject ToturialWindow;
   // public GameObject OverLay;
    public GameObject OverLay1;
    //public GameObject TakeFoodWindow;
    public GameObject FruitsNumberWindow;
    public Text FlagCounts;
    public Text FruitsCounts;
    public Text FruitsCounts2;
    public Text StepsCounts;
    public static int FlagNumber;
    public static int FruitsNumber;
    public static int StepsNumber;

    private static UI _instance;
    public static UI Instance
    {
        get
        {
            if (_instance == null)
            {
                UI singleton = GameObject.FindObjectOfType<UI>();
                if (singleton == null)
                {
                    GameObject go = new GameObject();
                    _instance = go.AddComponent<UI>();
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
        
      
       

    }

    private void Start()
    {
        GameOver.SetActive(false);
        LevelComplete.SetActive(false);
        PLayAgainButton.SetActive(false);
       // OverLay.SetActive(false);
        //FruitsNumberWindow.SetActive(false);
      //  TakeFoodWindow.SetActive(false);
        Toturial();

    }

    private void Update()
    {
       if (Input.GetKeyDown(KeyCode.Space))
        {
            AddtoFruitsCounte();
            AddtoFlagCounte();
        }
    }


    //Game Over and Level Complete
    public void CallGameOver()
    {
        GameOver.SetActive(true);
        PLayAgainButton.SetActive(true);
    }

    public void CallLevelComplete()
    {
        LevelComplete.SetActive(true);
        PLayAgainButton.SetActive(true);
    }

    //Flag Counters
    public void AddtoFlagCounte()
    {
        FlagNumber++;
        FlagCounts.text = FlagNumber.ToString();
    }


    //Fruit Counters
    public void AddtoFruitsCounte()
    {
        FruitsNumber++;
        FruitsCounts.text = FlagNumber.ToString();
        FruitsCounts2.text = FlagNumber.ToString();

    }


    //Steps Counters
    public void AddtoStepsCounte()
    {
        StepsNumber++;
        StepsCounts.text = FlagNumber.ToString();
    }

    public void Toturial()
    {

            if (Input.GetKey(KeyCode.Mouse0) && CloseButton)
            {

                ToturialWindow.SetActive(false);
                 
            }

    }
    


   

}
