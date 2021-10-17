using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
 {
    public string LoadGame;
    



    void Update()
    {
       if(Input.GetKey(KeyCode.Mouse0))
        {
            LoadGame = "SampleScene 1";
            SceneManager.LoadScene(LoadGame);
        }
    }
}
