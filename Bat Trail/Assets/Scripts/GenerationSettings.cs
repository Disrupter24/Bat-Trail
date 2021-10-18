using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerationSettings : MonoBehaviour
{
    [SerializeField] private bool LoadPreset;
    [System.NonSerialized] public int MapToLoad;
    [SerializeField] private Vector2 RandomGridSize;
    [SerializeField] private int RandomWallCount;
    [SerializeField] private int RandomFruitCount;
    [SerializeField] private int RandomPitfallCount;
    [SerializeField] private Vector2 GridSize; // Must be odd numbers or things break
    public static Vector2 StartPoint;
    [SerializeField] private Vector2[] WallLocations;
    [SerializeField] private Vector2[] FruitLocations;
    [SerializeField] private Vector2[] PitfallLocations;
    [SerializeField] private SpriteRenderer GridSpriteRenderer;
    [SerializeField] private Transform BatPosition;
    [SerializeField] private MapPreparation MapPreparation;
    [SerializeField] private int StepLimit;
    public int[] FruitRequirements;
    [SerializeField] public static int[] Requirements = new int[5];
    private int LeftLimit;
    private int RightLimit;
    private int LowerLimit;
    private int UpperLimit;
    private void Start()
    {
        Requirements = FruitRequirements;
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
        LoadMap();
        BatPosition.position = StartPoint;
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
    public static void LoadMap()
    {
        switch (Manager.MapNumber)
        {
            case 0:
                {
                    MapPreparation.WallLocations = MapArrays.WallsArray1;
                    MapPreparation.FruitLocations = MapArrays.FruitsArray1;
                    MapPreparation.PitfallLocations = MapArrays.PitfallsArray1;
                    break;
                }
            case 1:
                {
                    MapPreparation.WallLocations = MapArrays.WallsArray2;
                    MapPreparation.FruitLocations = MapArrays.FruitsArray2;
                    MapPreparation.PitfallLocations = MapArrays.PitfallsArray2;
                    break;
                }
            case 2:
                {
                    MapPreparation.WallLocations = MapArrays.WallsArray3;
                    MapPreparation.FruitLocations = MapArrays.FruitsArray3;
                    MapPreparation.PitfallLocations = MapArrays.PitfallsArray3;
                    break;
                }
            case 3:
                {
                    MapPreparation.WallLocations = MapArrays.WallsArray4;
                    MapPreparation.FruitLocations = MapArrays.FruitsArray4;
                    MapPreparation.PitfallLocations = MapArrays.PitfallsArray4;
                    break;
                }
            case 4:
                {
                    MapPreparation.WallLocations = MapArrays.WallsArray5;
                    MapPreparation.FruitLocations = MapArrays.FruitsArray5;
                    MapPreparation.PitfallLocations = MapArrays.PitfallsArray5;
                    break;
                }
        }
        StartPoint = MapArrays.SpawnPoint[Manager.MapNumber];
    }
}
