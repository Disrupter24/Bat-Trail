using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerationSettings : MonoBehaviour
{
    [SerializeField] private bool LoadPreset;
    [SerializeField] private Vector2 GridSize; // Must be odd numbers or things break
    [SerializeField] private Vector2 StartPoint;
    [SerializeField] private Vector2[] WallLocations;
    [SerializeField] private Vector2[] FruitLocations;
    [SerializeField] private Vector2[] PitfallLocations;
    [SerializeField] private SpriteRenderer GridSpriteRenderer;
    [SerializeField] private Transform BatPosition;
    [SerializeField] private MapPreparation MapPreparation;
    private void Start()
    {
        if (!LoadPreset)
        {
            //Randomization
        }
        GridSpriteRenderer.size = GridSize;
        // GridSpriteRenderer.enabled = false;
        BatPosition.position = StartPoint;
        MapPreparation.WallLocations = WallLocations;
        MapPreparation.FruitLocations = FruitLocations;
        MapPreparation.PitfallLocations = PitfallLocations;
        MapPreparation.enabled = true;
    }
}
