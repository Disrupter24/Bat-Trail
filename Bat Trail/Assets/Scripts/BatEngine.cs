using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BatEngine : MonoBehaviour
{
    [System.NonSerialized] public Vector2 TargetPos;
    private bool isMoving;
    private Vector3 Up = new Vector2(0, 1);
    private Vector3 Left = new Vector2(-1, 0);
    private Vector3 Down = new Vector2(0, -1);
    private Vector3 Right = new Vector2(1, 0);
    private Vector3 None = new Vector2(0, 0);
    private bool Slowdown = true;

    void Update()
    {
        if (!isMoving)
        {
            MoveInDirection(PullDirection());
        }
        else
        {
            if (Slowdown)
            {
                Slowdown = false;
                Invoke("MoveAgain", 0.25f);
            }
        }
    }
    private Vector3 PullDirection() // Checks user input for wasd|arrow keys, returns a string for the direction the player wants to go.
    {
        if (Input.GetKeyDown("w") | Input.GetKeyDown("up"))
        {
            isMoving = true;
            return Up;
        }
        else if (Input.GetKeyDown("a") | Input.GetKeyDown("left"))
        {
            isMoving = true;
            return Left;
        }
        else if (Input.GetKeyDown("s") | Input.GetKeyDown("down"))
        {
            isMoving = true;
            return Down;
        }
        else if (Input.GetKeyDown("d") | Input.GetKeyDown("right"))
        {
            isMoving = true;
            return Right;
        }
        else
        {
            return None;
        }
    }

    private void MoveInDirection(Vector3 Direction)
    {        
        if (Direction != None)
        {
            TargetPos = transform.position + Direction;
            WallCheck();
            CheckTarget(MapPreparation.WallLocations);
            transform.DOMove(TargetPos, 0.2f);
        }
    }
    private void MoveAgain()
    {
        Slowdown = true;
        isMoving = false;
    }
    private void WallCheck()
    {
        if (TargetPos.x >= MapPreparation.RightBorder | TargetPos.x <= MapPreparation.LeftBorder | TargetPos.y >= MapPreparation.TopBorder | TargetPos.y <= MapPreparation.BottomBorder)
        {
            Debug.Log("Out of bounds, moving back");
            TargetPos = gameObject.transform.position;
        }
    }
    private void CheckTarget(Vector2[] Target)
    {
        if (Target != null)
        {
            for (int i = 0; i < Target.Length; i++)
            {
                if (TargetPos == Target[i])
                {
                    Debug.Log("Hit Wall, moving back");
                    TargetPos = gameObject.transform.position;
                }
            }
        }
        else
        {
            return;
        }
        
    }
}
