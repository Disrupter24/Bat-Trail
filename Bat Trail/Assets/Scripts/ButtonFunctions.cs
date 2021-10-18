using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonFunctions : MonoBehaviour
{
    public GameObject MinimizeTarget;
    public BatEngine BatEngine;
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void Minimize()
    {
        MinimizeTarget.SetActive(false);
    }
    public void EnableBat()
    {
        BatEngine.enabled = true;
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }
}
