using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BatEngine : MonoBehaviour
{
    [System.NonSerialized] public Vector2 TargetPos;
    private bool isMoving;
    private Vector2 Up = new Vector2(0, 1);
    private Vector2 Left = new Vector2(-1, 0);
    private Vector2 Down = new Vector2(0, -1);
    private Vector2 Right = new Vector2(1, 0);
    private Vector2 None = new Vector2(0, 0);
    private bool Slowdown = true;
    private int TriggerNumber;
    private int StepsTaken;
    public static int StepLimit;

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

    private void MoveInDirection(Vector2 Direction)
    {        
        if (Direction != None)
        {
            TargetPos = new Vector2(transform.position.x, transform.position.y) + Direction;
            BoundsCheck();
            if (CheckTarget(MapPreparation.WallLocations))
            {
                TargetPos = transform.position;
            }
            if (CheckTarget(MapPreparation.FruitLocations))
            {
                Manager.FruitScore++;
                ClearObject(TriggerNumber, MapPreparation.FruitObjects, MapPreparation.FruitLocations);
                Debug.Log("Fruits Collected: " + Manager.FruitScore + " / " + MapPreparation.FruitLocations.Length);

            }
            if (CheckTarget(MapPreparation.PitfallLocations))
            {
                ClearObject(TriggerNumber, MapPreparation.PitfallObjects, MapPreparation.PitfallLocations);
                Debug.Log("Stepped on Pitfall");
                //EndTurn();
            }
            if (TargetPos != new Vector2 (transform.position.x, transform.position.y)) // This script will be executed only when the player moves successfully (is not blocked).
            {
                OnMove();
            }    
            transform.DOMove(TargetPos, 0.2f);
        }
    }
    private void MoveAgain()
    {
        Slowdown = true;
        isMoving = false;
    }
    private void BoundsCheck()
    {
        if (TargetPos.x >= MapPreparation.RightBorder | TargetPos.x <= MapPreparation.LeftBorder | TargetPos.y >= MapPreparation.TopBorder | TargetPos.y <= MapPreparation.BottomBorder)
        {
            TargetPos = gameObject.transform.position;
        }
    }
    private bool CheckTarget(Vector2[] Target)
    {
        if (Target != null)
        {
            for (int i = 0; i < Target.Length; i++)
            {
                if (TargetPos == Target[i])
                {
                    TriggerNumber = i;
                    return true;
                }
            }
            return false;
        }
        else
        {
            return false;
        }
        
    }
    private void ClearObject(int ObjectToClear, GameObject[]ArrayToClear, Vector2[] VectorArrayToClear)
    {
        if (ArrayToClear[ObjectToClear].GetComponent<AudioSource>() != null)
        {
            ArrayToClear[ObjectToClear].GetComponent<AudioSource>().enabled = false;
        }
        if (ArrayToClear[ObjectToClear].GetComponent<SpriteRenderer>() != null)
        {
           ArrayToClear[ObjectToClear].GetComponent<SpriteRenderer>().enabled = true;
        }
        for (int i = ObjectToClear; i < ArrayToClear.Length - 1; i++)
        {
            VectorArrayToClear[i] = VectorArrayToClear[i + 1];
            ArrayToClear[i] = ArrayToClear[i + 1];
        }
        Array.Resize<Vector2>(ref VectorArrayToClear, VectorArrayToClear.Length - 1);
        Array.Resize<GameObject>(ref ArrayToClear, ArrayToClear.Length - 1);
    }
    private void PlaySounds(GameObject[] TargetArray)
    {
        for (int i = 0; i < TargetArray.Length; i++)
        {
            TargetArray[i].GetComponent<AudioSource>().Play();
        }
    }
    private void OnMove()
    {
        StepsTaken++;
        StepCheck();
        PlaySounds(MapPreparation.FruitObjects);
        PlaySounds(MapPreparation.PitfallObjects);
    }
    private void StepCheck()
    {
        if (StepsTaken == StepLimit)
        {
            Debug.Log("You've taken " + StepLimit + " steps.");
        }
    }
}
