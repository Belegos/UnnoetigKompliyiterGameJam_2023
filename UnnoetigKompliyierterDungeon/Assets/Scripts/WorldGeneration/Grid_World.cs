using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid_World : MonoBehaviour
{
    private Vector2Int[,] _placementGrid;

    public Grid_World(int height, int width) 
    {
        _placementGrid = new Vector2Int[height, width];
    }
    public Vector2Int[,] GetPlacementGrid 
    {
     get { return _placementGrid; }
    }

}
