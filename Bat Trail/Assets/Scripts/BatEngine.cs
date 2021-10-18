using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class BatEngine : MonoBehaviour
{
    [System.NonSerialized] public Vector2 TargetPos;
    private bool isMoving;
    private Vector2 Up = new Vector2(0, 1);
    private Vector2 Left = new Vector2(-1, 0);
    private Vector2 Down = new Vector2(0, -1);
    private Vector2 Right = new Vector2(1, 0);
    private Vector2 None = new Vector2(0, 0);
    [System.NonSerialized] public static int FlagMax = 3;
    [System.NonSerialized] public static bool BatRun = true;
    private bool Slowdown = true;
    private int TriggerNumber;
    private int StepsTaken;
    public static int StepLimit;
    public GameObject DeathScreen;
    public Text FruitCounter;
    public Text StepCounter;
    public Text FlagCounter;
    public Text[] NumberTexts;
    public GameObject VoteMenu;
    public GameObject WinScreen;

    private void Awake()
    {
        MapPreparation.FlagsRemaining = FlagMax;
        FlagCounter.text = MapPreparation.FlagsRemaining.ToString();
    }
    void Update()
    {
        if (BatRun)
        {
            FlagCheck();
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
            if (!BoundsCheck(TargetPos))
            {
                TargetPos = new Vector2(transform.position.x, transform.position.y);
            }
            if (CheckTarget(MapPreparation.WallLocations))
            {
                TargetPos = transform.position;
            }
            if (CheckTarget(MapPreparation.FruitLocations))
            {
                Manager.FruitScore[Manager.PlayerTurn]++;
                ClearObject(TriggerNumber, MapPreparation.FruitObjects, MapPreparation.FruitLocations);
                FruitCounter.text = Manager.FruitScore[Manager.PlayerTurn].ToString();

            }
            if (CheckTarget(MapPreparation.PitfallLocations))
            {
                ClearObject(TriggerNumber, MapPreparation.PitfallObjects, MapPreparation.PitfallLocations);
                Debug.Log("Stepped on Pitfall");
                EndTurn();
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
    private bool BoundsCheck(Vector2 ToCheck)
    {
        if (ToCheck.x >= MapPreparation.RightBorder | ToCheck.x <= MapPreparation.LeftBorder | ToCheck.y >= MapPreparation.TopBorder | ToCheck.y <= MapPreparation.BottomBorder)
        {
            return false;
        }
        else
        {
            return true;
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
        StepCounter.text = (StepLimit - StepsTaken).ToString();
        if (StepsTaken == StepLimit)
        {
            EndTurn();
        }
    }
    private void FlagCheck()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Vector2 CursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (BoundsCheck(CursorPos))
            {
                Vector2 CursorSquare = CursorSquareFind(CursorPos);
                MapPreparation.FlagHandler(CursorSquare);
            }
            else
            {
                return;
            }
        }
    }
    private Vector2 CursorSquareFind(Vector2 ToSquare)
    {
        Vector2 RoundedPoint = new Vector2(Mathf.RoundToInt(ToSquare.x), Mathf.RoundToInt(ToSquare.y));
        return RoundedPoint;
    }
    private void EndTurn()
    {
        MapPreparation.FlagsRemaining = FlagMax;
        StepsTaken = 0;
        BatRun = false;
        DeathScreen.SetActive(true);
        DeathScreen.GetComponent<ShareMenu>().Cycle();
        if (Manager.PlayerLeftPack[Manager.PlayerTurn])
        {
            DeathScreen.GetComponent<ShareMenu>().Submit();
        }
    }
    public void VoteTextUpdate()
    {
        for (int i = 0; i < NumberTexts.Length; i++)
        {
            NumberTexts[i].text = Manager.SharedLastRound[i].ToString();
        }
    }
}
