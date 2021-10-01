using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BatEngine : MonoBehaviour
{
    [SerializeField] public Vector3 TargetPos;
    private bool isMoving;
    private Vector3 Up = new Vector3(0, 1, 0);
    private Vector3 Left = new Vector3(-1, 0, 0);
    private Vector3 Down = new Vector3(0, -1, 0);
    private Vector3 Right = new Vector3(1, 0, 0);
    private Vector3 None = new Vector3(0, 0, 0);
    private bool Slowdown = true;

    void Update()
    {
        if (!isMoving)
        {
            SetTargetPos(PullDirection());
        }
        else
        {
            if (Slowdown)
            {
                Slowdown = false;
                Invoke("MoveAgain", 0.5f);
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

    private void SetTargetPos(Vector3 Direction)
    {
        TargetPos = transform.position + Direction;
        transform.DOMove(TargetPos, 0.5f);
        if (Direction != None)
        {
            MoveCheck();
        }
    }
    private void MoveAgain()
    {
        Slowdown = true;
        isMoving = false;
    }
    private void MoveCheck()
    {
        Debug.Log("Has Moved");
    }
}
