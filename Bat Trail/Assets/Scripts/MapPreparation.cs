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
    private int WallCount = 3;
    private Object WallPrefab;
    private Object FruitPrefab;
    private Object PitfallPrefab;
    void Start()
    {
        SetBorders(); //NOTE: This script will assume that the box is perfectly rounded to the grid and centered on 0, otherwise some jank might occur.
        WallHandler();
        FruitHandler();
        PitfallHandler();
    }

    private void SetBorders()
    {
        GridSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        TopBorder = Mathf.RoundToInt(0.5f + transform.position.y + (GridSpriteRenderer.size.y / 2));
        Debug.Log(TopBorder);
        LeftBorder = Mathf.RoundToInt(-0.5f + transform.position.x - (GridSpriteRenderer.size.x / 2));
        Debug.Log(LeftBorder);
        BottomBorder = Mathf.RoundToInt(-0.5f + transform.position.y - (GridSpriteRenderer.size.y / 2));
        Debug.Log(BottomBorder);
        RightBorder = Mathf.RoundToInt(0.5f + transform.position.x + (GridSpriteRenderer.size.x / 2));
        Debug.Log(RightBorder);
    }
    private void WallHandler()
    {
        WallPrefab = Resources.Load("prefabs/Wall", typeof(GameObject));
        SpawnTime(WallLocations, WallPrefab);
    }
    private void FruitHandler()
    {
        FruitPrefab = Resources.Load("prefabs/Fruit", typeof(GameObject));
        SpawnTime(FruitLocations, FruitPrefab);
    }
    private void PitfallHandler()
    {
        PitfallPrefab = Resources.Load("prefabs/Pitfall", typeof(GameObject));
        SpawnTime(PitfallLocations, PitfallPrefab);
    }
    void SpawnTime(Vector2[] SpawnType, Object Prefab)
    {
        if(SpawnType != null && Prefab!= null)
        {
            for (int i = 0; i < SpawnType.Length; i++)
            {
                Instantiate(Prefab, SpawnType[i], Quaternion.identity);
            }
        }
    }
}
