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
<<<<<<< Updated upstream
=======
    private int TriggerNumber;
    private int StepsTaken;
    public static int StepLimit;
>>>>>>> Stashed changes

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
        TargetPos = transform.position + Direction;
        WallCheck();
        
        transform.DOMove(TargetPos, 0.2f);
        if (Direction != None)
        {
            OnMove();
        }
    }
    private void MoveAgain()
    {
        Slowdown = true;
        isMoving = false;
    }
    private void OnMove()
    {
        BoundsCheck();
    }
    private void WallCheck()
    {
        if (TargetPos.x >= BoundsFinder.RightBorder)
        {
            TargetPos = new Vector2(BoundsFinder.RightBorder - 1, TargetPos.y);
        }
        else if (TargetPos.x <= BoundsFinder.LeftBorder)
        {
            TargetPos = new Vector2(BoundsFinder.LeftBorder + 1, TargetPos.y);
        }
<<<<<<< Updated upstream
        else if (TargetPos.y >= BoundsFinder.TopBorder)
=======
        if (MapPreparation.FruitObjects[FruitToClear].GetComponent<SpriteRenderer>() != null)
        {
            MapPreparation.FruitObjects[FruitToClear].GetComponent<SpriteRenderer>().enabled = false;
        }
        for (int i = FruitToClear; i < MapPreparation.FruitLocations.Length - 1; i++)
>>>>>>> Stashed changes
        {
            TargetPos = new Vector2(TargetPos.x, BoundsFinder.TopBorder - 1);
        }
        else if (TargetPos.y <= BoundsFinder.BottomBorder)
        {
            TargetPos = new Vector2(TargetPos.x, BoundsFinder.BottomBorder + 1);
        }
    }
    private void BoundsCheck()
    {
<<<<<<< Updated upstream
        if (transform.position.x > BoundsFinder.RightBorder)
        {
            transform.position = new Vector2(BoundsFinder.RightBorder - 1, transform.position.y);
            Debug.Log("Hit Wall, moving back");
=======
        if (StepsTaken == StepLimit)
        {
            Debug.Log("You've taken " + StepLimit + " steps.");
>>>>>>> Stashed changes
        }
    }
}
