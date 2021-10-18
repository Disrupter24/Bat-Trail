using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPreparation : MonoBehaviour
{
    public static int TopBorder = 31;
    public static int LeftBorder = -1;
    public static int BottomBorder = -1;
    public static int RightBorder = 31;
    private SpriteRenderer GridSpriteRenderer;
    public static Vector2[] WallLocations;
    public static Vector2[] FruitLocations;
    public static Vector2[] PitfallLocations;
    public static Vector2[] FlagLocations;
    private static UnityEngine.Object WallPrefab;
    private static UnityEngine.Object FruitPrefab;
    private static UnityEngine.Object PitfallPrefab;
    private static UnityEngine.Object FlagPrefab;
    public static  GameObject[] WallObjects;
    public static GameObject[] FruitObjects;
    public static  GameObject[] PitfallObjects;
    public static GameObject[] FlagObjects;
    private static int TargetFlag;
    public static int FlagsRemaining;
    void Start()
    {
        //SetBorders(); //NOTE: This script will assume that the box is perfectly rounded to the grid and centered on 0, otherwise some jank might occur.
        // Borders will be set manually for the current game version.
        WallObjects = new GameObject[WallLocations.Length];
        FruitObjects = new GameObject[FruitLocations.Length];
        PitfallObjects = new GameObject[PitfallLocations.Length];
        FlagObjects = new GameObject[4 * BatEngine.FlagMax];
        FlagLocations = new Vector2[FlagObjects.Length];
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
    public static void FlagHandler(Vector2 TargetPos)
    {
        FlagPrefab = Resources.Load("prefabs/Flag", typeof(GameObject));
        if (CheckV2Array(FlagLocations, TargetPos))
        {
            FlagLocations[TargetFlag] = Vector2.zero;
            FlagsRemaining++;
            GameObject.Find("Bat").GetComponent<BatEngine>().FlagCounter.text = FlagsRemaining.ToString();
            Destroy(FlagObjects[TargetFlag]);
        }
        else if (FlagsRemaining > 0)
        {
            PlaceFlag(TargetPos);
        }
    }
    private void SpawnTime(Vector2[] SpawnType, UnityEngine.Object Prefab, GameObject[] ObjectArray)
    {
        if(SpawnType != null && Prefab!= null)
        {
            for (int i = 0; i < SpawnType.Length; i++)
            {
                ObjectArray[i] = Instantiate(Prefab, SpawnType[i], Quaternion.identity) as GameObject;
            }
        }
    }
    private static bool CheckV2Array(Vector2[] ArrayToBeChecked, Vector2 KeyVector)
    {
        for (int i = 0; i < ArrayToBeChecked.Length; i++)
        {
            if (ArrayToBeChecked[i] == KeyVector)
            {
                TargetFlag = i;
                return true;
            }
        }
        return false;
    }
    private static void PlaceFlag(Vector2 PlacementCoords)
    {
        for (int i = 0; i < FlagLocations.Length; i++)
        {
            if (FlagLocations[i] == Vector2.zero)
            {
                FlagLocations[i] = PlacementCoords;
                FlagObjects[i] = Instantiate(FlagPrefab, PlacementCoords, Quaternion.identity) as GameObject;
                FlagsRemaining--;
                GameObject.Find("Bat").GetComponent<BatEngine>().FlagCounter.text = FlagsRemaining.ToString();
                return;
            }
        }
    }

}
