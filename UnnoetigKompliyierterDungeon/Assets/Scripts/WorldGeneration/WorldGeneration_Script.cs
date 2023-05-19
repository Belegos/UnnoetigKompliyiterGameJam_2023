using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WorldGeneration_Script : MonoBehaviour
{
    [SerializeField] SO_WorldData _data;
    [SerializeField] private int _height;
    [SerializeField] private int _width;
    private int _maxWidth;
    private int _maxHeight;
    [SerializeField] private float _wallOffset = -0.1f;
    Grid_World _worldGrid;
    private Transform _thisTransform;
    private GameObject _placeThis;

    void Start()
    {
        _worldGrid = new Grid_World(_height, _width);
        _maxHeight = _height;
        _maxWidth = _width;
        _thisTransform = GetComponent<Transform>();
        this.transform.position = new Vector3(0, 0, 0);
        GenerateGrid(_worldGrid, _data);
    }

    public void GenerateGrid(Grid_World _worldGrid, SO_WorldData _data)
    {
        if (_worldGrid == null)
        {
            Debug.Log("There were no grid");
            return;
        }

        Vector2Int[,] _gridMap = _worldGrid.GetPlacementGrid;
        int indexHeight = 0;
        int indexWidth = 0;
        int rowCount = 1;

        for (int i = 0; i < _width; i++) // y achse
        {
            GameObject currentRow = new GameObject($"Row_" + rowCount.ToString());
            currentRow.transform.parent = _thisTransform;
            //currentRow.name = "Row_" + _width.ToString();
            int offSetZTileOne = +4; //off set Position of the first tile
            int offSetZTileTwo = +1; //off set Position of the second tile
            int offSetZTileThree = -2; //off set Position of the third tile
            for (int j = 0; j < _height; j++) // x achse
            {
                if (indexHeight == 0)
                {
                    int rndIndex = Random.Range(0, _data.prefab_trueTrap.Length);
                    _placeThis = Instantiate(_data.prefab_tile[rndIndex], currentRow.transform);
                    _placeThis.transform.position = new Vector3(indexWidth * 2, 0, indexHeight + offSetZTileOne);
                    //roof 1
                    _placeThis = Instantiate(_data.Wall[1], currentRow.transform);
                    _placeThis.transform.position = new Vector3(indexWidth * 2, 3, indexHeight + offSetZTileOne);
                }

                if (indexHeight == 1)
                {
                    int rndIndex = Random.Range(0, _data.prefab_trueTrap.Length);
                    _placeThis = Instantiate(_data.prefab_tile[rndIndex], currentRow.transform);
                    _placeThis.transform.position = new Vector3(indexWidth * 2, 0, indexHeight + offSetZTileTwo);
                    //roof 2
                    _placeThis = Instantiate(_data.Wall[1], currentRow.transform);
                    _placeThis.transform.position = new Vector3(indexWidth * 2, 3, indexHeight + offSetZTileTwo);                    
                }

                if (indexHeight == 2)
                {
                    int rndIndex = Random.Range(0, _data.prefab_trueTrap.Length);
                    _placeThis = Instantiate(_data.prefab_tile[rndIndex], currentRow.transform);
                    _placeThis.transform.position = new Vector3(indexWidth * 2, 0, indexHeight + offSetZTileThree);
                    //roof 3
                    _placeThis = Instantiate(_data.Wall[1], currentRow.transform);
                    _placeThis.transform.position = new Vector3(indexWidth * 2, 3, indexHeight + offSetZTileThree);
                }

                indexHeight++;
                if (indexHeight == _maxHeight)
                {
                    _placeThis = Instantiate(_data.Wall[0], currentRow.transform);
                    _placeThis.transform.position = new Vector3(indexWidth * 2, _wallOffset, indexHeight * 2); //left wall
                    _placeThis = Instantiate(_data.Wall[0], currentRow.transform);
                    _placeThis.transform.position = new Vector3(indexWidth * 2, _wallOffset, 0);                //right wall
                    
                    indexHeight = 0;
                    indexWidth++;
                    rowCount++;
                }
            }
        }

        Debug.Log("Generated Grid: " + _height + "," + _width + "in GameObject: ", gameObject);
    }
}