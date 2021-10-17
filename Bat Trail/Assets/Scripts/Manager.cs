using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    private static Manager _instance;
    [System.NonSerialized] public static int[] FruitScore = new int[4];
    public static int PlayerTurn = 0;
    public static int MapNumber = 0;
    public static bool[] PlayerGone;
    public static int[] StashCount = new int[4];
    public static int SharedFruitTotal;
    public Color[] PlayerColors;

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
    private void Start()
    {
    }
}
