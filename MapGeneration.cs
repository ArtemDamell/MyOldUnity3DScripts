﻿using UnityEngine;
using System.Collections;
using System;

public class MapGeneration : MonoBehaviour
{
    [Range(0.2f, 1f)] public float sqSize;
    [Range(0, 100)] public int RandomFillPercent;

    public int width;
    public int height;
    public int xt, zt;
    public Vector3 fw;
    public string seed;
    public bool UseRandomSeed;
    public int[,] map;

    private void Start()
    {
        GenerateMap();
    }

    /// <summary>
    /// Generates a map with random values and smooths it.
    /// </summary>
    /// <returns>
    /// A map with random values and smoothed.
    /// </returns>
    private void GenerateMap()
    {
        map = new int[width, height];
        RandomFillMap();

        for (int i = 0; i < 5; i++)
        {
            SmoothMap();
        }

        MeshGeneration mashGen = GetComponent<MeshGeneration>();
        mashGen.GenerateMesh(map, 1f); // 0.1f
    }

    /// <summary>
    /// Fills the map with random values based on the given seed and RandomFillPercent.
    /// </summary>
    private void RandomFillMap()
    {
        if (UseRandomSeed)
        {
            seed = Time.time.ToString();
        }

        System.Random PseudoRandom = new System.Random(seed.GetHashCode());

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++) //width
            {
                if ((x == 0) || (width == -1) || (y == 0) || (y == height - 1))
                {
                    map[x, y] = 1;
                }
                else
                {
                    map[x, y] = (PseudoRandom.Next(0, 100) < RandomFillPercent) ? 1 : 0;
                }
            }
        }
    }

    /// <summary>
    /// Smooths the map by setting tiles to 1 or 0 based on the number of surrounding wall tiles.
    /// </summary>
    /// <returns>
    /// Void.
    /// </returns>
    private void SmoothMap()
    {
        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
            {
                int neighbourWallTiles = GetSorroundWallCount(x, y);

                if (neighbourWallTiles > 4)
                    map[x, y] = 1;
                else if (neighbourWallTiles < 4)
                    map[x, y] = 0;
            }
    }

    /// <summary>
    /// Gets the number of walls surrounding a given grid coordinate.
    /// </summary>
    /// <param name="GridX">The x coordinate of the grid.</param>
    /// <param name="GridY">The y coordinate of the grid.</param>
    /// <returns>The number of walls surrounding the given grid coordinate.</returns>
    private int GetSorroundWallCount(int GridX, int GridY)
    {
        int WallCount = 0;
        for (int neighbourX = GridX - 1; neighbourX <= GridX + 1; neighbourX++)
            for (int neighbourY = GridY - 1; neighbourY <= GridY + 1; neighbourY++)
            {
                if ((neighbourX >= 0) && (neighbourX < width) && (neighbourY >= 0) && (neighbourY < height))
                {
                    if ((neighbourX != GridX) || (neighbourY != GridY))
                        WallCount += map[neighbourX, neighbourY];
                }
                else
                    WallCount++;
            }
        return WallCount;
    }
}