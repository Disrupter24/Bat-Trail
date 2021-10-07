using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPreparation : MonoBehaviour
{
    public static int TopBorder;
    public static int LeftBorder;
    public static int BottomBorder;
    public static int RightBorder;
    private SpriteRenderer GridSpriteRenderer;
    public static Vector2[] WallLocations;
    public static Vector2[] FruitLocations;
    public static Vector2[] PitfallLocations;
    private UnityEngine.Object WallPrefab;
    private UnityEngine.Object FruitPrefab;
    private UnityEngine.Object PitfallPrefab;
    public static  GameObject[] WallObjects;
    public static GameObject[] FruitObjects;
    public static  GameObject[] PitfallObjects;
    void Start()
    {
        SetBorders(); //NOTE: This script will assume that the box is perfectly rounded to the grid and centered on 0, otherwise some jank might occur.
        WallObjects = new GameObject[WallLocations.Length];
        FruitObjects = new GameObject[FruitLocations.Length];
        PitfallObjects = new GameObject[PitfallLocations.Length];
        WallHandler();
        FruitHandler();
        PitfallHandler();
    }

    private void SetBorders()
    {
        GridSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        TopBorder = Mathf.RoundToInt(0.5f + transform.position.y + (GridSpriteRenderer.size.y / 2));
        LeftBorder = Mathf.RoundToInt(-0.5f + transform.position.x - (GridSpriteRenderer.size.x / 2));
        BottomBorder = Mathf.RoundToInt(-0.5f + transform.position.y - (GridSpriteRenderer.size.y / 2));
        RightBorder = Mathf.RoundToInt(0.5f + transform.position.x + (GridSpriteRenderer.size.x / 2));
    }
    private void WallHandler()
    {
        WallPrefab = Resources.Load("prefabs/Wall", typeof(GameObject));
        SpawnTime(WallLocations, WallPrefab, WallObjects);
    }
    private void FruitHandler()
    {
        FruitPrefab = Resources.Load("prefabs/Fruit", typeof(GameObject));
        SpawnTime(FruitLocations, FruitPrefab, FruitObjects);
    }
    private void PitfallHandler()
    {
        PitfallPrefab = Resources.Load("prefabs/Pitfall", typeof(GameObject));
        SpawnTime(PitfallLocations, PitfallPrefab, PitfallObjects);
    }
    void SpawnTime(Vector2[] SpawnType, UnityEngine.Object Prefab, GameObject[] ObjectArray)
    {
        if(SpawnType != null && Prefab!= null)
        {
            for (int i = 0; i < SpawnType.Length; i++)
            {
                ObjectArray[i] = Instantiate(Prefab, SpawnType[i], Quaternion.identity) as GameObject;
            }
        }
    }
}
