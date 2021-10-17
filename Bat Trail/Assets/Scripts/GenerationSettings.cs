using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerationSettings : MonoBehaviour
{
    [SerializeField] private bool LoadPreset;
    [SerializeField] private Vector2 RandomGridSize;
    [SerializeField] private int RandomWallCount;
    [SerializeField] private int RandomFruitCount;
    [SerializeField] private int RandomPitfallCount;
    [SerializeField] private Vector2 GridSize; // Must be odd numbers or things break
    [SerializeField] private Vector2 StartPoint;
    [SerializeField] private Vector2[] WallLocations;
    [SerializeField] private Vector2[] FruitLocations;
    [SerializeField] private Vector2[] PitfallLocations;
    [SerializeField] private SpriteRenderer GridSpriteRenderer;
    [SerializeField] private Transform BatPosition;
    [SerializeField] private MapPreparation MapPreparation;
    [SerializeField] private int StepLimit;
    [SerializeField] public static int[] Requirements;
    private int LeftLimit;
    private int RightLimit;
    private int LowerLimit;
    private int UpperLimit;
    private void Start()
    {
        if (!LoadPreset)
        {
            ResetArray(WallLocations, RandomWallCount);
            ResetArray(FruitLocations, RandomFruitCount);
            ResetArray(PitfallLocations, RandomPitfallCount);
            GridSize = RandomGridSize;
            Randomization();
        }
        GridSpriteRenderer.size = GridSize;
        GridSpriteRenderer.enabled = false;
        BatPosition.position = StartPoint;
        MapPreparation.WallLocations = WallLocations;
        MapPreparation.FruitLocations = FruitLocations;
        MapPreparation.PitfallLocations = PitfallLocations;
        BatEngine.StepLimit = StepLimit;
        MapPreparation.enabled = true;
    }
    private void Randomization() // Everything in here is pretty horribly optimized, but it only runs once, so it'll have to do.
    {
        FindLimits();
        StartPoint = RandomStartPoint();
        WallLocations = new Vector2[RandomWallCount];
        FruitLocations = new Vector2[RandomFruitCount];
        PitfallLocations = new Vector2[RandomPitfallCount];
        RandomType(WallLocations, RandomWallCount);
        RandomType(FruitLocations, RandomFruitCount);
        RandomType(PitfallLocations, RandomPitfallCount);
    }
    private void FindLimits()
    {
        LeftLimit = Mathf.RoundToInt(transform.position.x - (GridSpriteRenderer.size.x / 2));
        RightLimit = Mathf.RoundToInt(transform.position.x + (GridSpriteRenderer.size.x / 2));
        UpperLimit = Mathf.RoundToInt(transform.position.y + (GridSpriteRenderer.size.y / 2));
        LowerLimit = Mathf.RoundToInt(transform.position.y - (GridSpriteRenderer.size.y / 2));
    }
    private Vector2 RandomStartPoint()
    {
        int RandomX = Random.Range(LeftLimit, RightLimit);
        int RandomY = Random.Range(LowerLimit, UpperLimit);
        return new Vector2(RandomX, RandomY);
    }
    private void RandomType(Vector2[] Locations, int Count)
    {
        for (int i = 0; i < Count; i++)
        {
            if (Locations[i] == new Vector2(0,0))
            {
                int RandomX = Random.Range(LeftLimit, RightLimit);
                int RandomY = Random.Range(LowerLimit, UpperLimit);
                Vector2 Prospect = new Vector2(RandomX, RandomY);
                if (RepeatCheck(WallLocations, Prospect) && RepeatCheck(FruitLocations, Prospect) && RepeatCheck(PitfallLocations, Prospect) && Prospect != StartPoint)
                {
                    Locations[i] = Prospect;
                }
                else
                {
                    RandomType(Locations, Count);
                }
            }
        }
    }
    private bool RepeatCheck(Vector2[] Locations, Vector2 Prospect)
    {
        for (int i = 0; i < Locations.Length; i++)
        {
            if (Prospect == Locations[i])
            {
                return false;
            }
        }
        return true;
    }
    private void ResetArray(Vector2[] Locations, int ArraySize)
    {
        Locations = new Vector2[0];
        Locations = new Vector2[ArraySize];
    }
}
