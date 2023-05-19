using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid_World
{
    private Vector2Int[,] _placementGrid;
    private int maxHeight;
    private int maxWidth;
    public Grid_World(int height, int width)
    {
        maxHeight = height;
        maxWidth = width;
        _placementGrid = new Vector2Int[height, width];
    }
    public Vector2Int[,] GetPlacementGrid 
    {
     get { return _placementGrid; }
    }
}
