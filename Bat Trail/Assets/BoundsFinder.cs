using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundsFinder : MonoBehaviour
{
    //NOTE: This script will assume that the box is perfectly rounded to the grid and centered on 0, otherwise some jank might occur.
    public static int TopBorder;
    public static int LeftBorder;
    public static int BottomBorder;
    public static int RightBorder;
    private SpriteRenderer GridSpriteRenderer;
    void Start()
    {
        GridSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
       // GridSpriteRenderer.enabled = false;
        GetBorders();
    }

    private void GetBorders()
    {
        TopBorder = Mathf.RoundToInt(transform.position.y + (GridSpriteRenderer.size.y / 2));
        Debug.Log(TopBorder);
        LeftBorder = Mathf.RoundToInt(transform.position.x - (GridSpriteRenderer.size.x / 2));
        Debug.Log(LeftBorder);
        BottomBorder = Mathf.RoundToInt(transform.position.y - (GridSpriteRenderer.size.y / 2));
        Debug.Log(BottomBorder);
        RightBorder = Mathf.RoundToInt(transform.position.x + (GridSpriteRenderer.size.x / 2));
        Debug.Log(RightBorder);
    }
}
