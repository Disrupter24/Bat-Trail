using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    private static Manager _instance;
    [System.NonSerialized] public static int FruitScore;

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
}
