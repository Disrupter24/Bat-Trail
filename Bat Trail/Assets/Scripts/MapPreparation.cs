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
    public bool LoadPreset = true;
    public static Vector2[] WallLocations;
    public static Vector2[] FruitLocations;
    public static Vector2[] HoleLocations;
    private int WallCount = 3;
    private Object WallPrefab;
    void Start()
    {
        SetBorders(); //NOTE: This script will assume that the box is perfectly rounded to the grid and centered on 0, otherwise some jank might occur.
        WallHandler();
    }

    private void SetBorders()
    {
        GridSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        // GridSpriteRenderer.enabled = false;
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
        WallLocations = new Vector2[WallCount];
        if (LoadPreset) // Contains the locations of preset walls in the first map layout
        {
            WallLocations[0] = new Vector2(4, 4);
            WallLocations[1] = new Vector2(1, 1);
            WallLocations[2] = new Vector2(2, 2);
        }
        SpawnTime(WallLocations);
    }
    void SpawnTime(Vector2[] SpawnType)
    {
        for (int i = 0; i < SpawnType.Length; i++)
        {
            Instantiate(WallPrefab, SpawnType[i], Quaternion.identity);
        }
    }
}
