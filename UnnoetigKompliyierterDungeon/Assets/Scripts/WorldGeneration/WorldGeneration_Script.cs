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

        _placeThis = Instantiate(_data.Start, _thisTransform);
        _placeThis.transform.name = "001_Start";

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

            var one = _data.prefab_trueTrap;
            int rndIndex = 0;
            for (int j = 0; j < _height; j++) // x achse
            {
                if (indexHeight == 0)
                {
                    one = ChooseRndArrayTile(_data, one);
                    //floortile 1
                    rndIndex = Random.Range(0, one.Length);
                    _placeThis = Instantiate(one[rndIndex], currentRow.transform);
                    _placeThis.transform.position = new Vector3(indexWidth * 2, 0, indexHeight + offSetZTileOne);
                    //roof 1
                    _placeThis = Instantiate(_data.Wall[1], currentRow.transform);
                    _placeThis.transform.position = new Vector3(indexWidth * 2, 3, indexHeight + offSetZTileOne);
                }

                if (indexHeight == 1)
                {
                    one = ChooseRndArrayTile(_data, one);
                    //floortile2
                    rndIndex = Random.Range(0, one.Length);
                    _placeThis = Instantiate(one[rndIndex], currentRow.transform);
                    _placeThis.transform.position = new Vector3(indexWidth * 2, 0, indexHeight + offSetZTileTwo);
                    //roof 2
                    _placeThis = Instantiate(_data.Wall[1], currentRow.transform);
                    _placeThis.transform.position = new Vector3(indexWidth * 2, 3, indexHeight + offSetZTileTwo);
                }


                if (indexHeight == 2)
                {
                    one = ChooseRndArrayTile(_data, one);
                    //floortile 3
                    rndIndex = Random.Range(0, one.Length);
                    _placeThis = Instantiate(one[rndIndex], currentRow.transform);
                    _placeThis.transform.position = new Vector3(indexWidth * 2, 0, indexHeight + offSetZTileThree);
                    //roof 3
                    _placeThis = Instantiate(_data.Wall[1], currentRow.transform);
                    _placeThis.transform.position = new Vector3(indexWidth * 2, 3, indexHeight + offSetZTileThree);
                }

                indexHeight++;
                if (indexHeight == _maxHeight)
                {
                    _placeThis = Instantiate(_data.Wall[0], currentRow.transform);
                    _placeThis.transform.position =
                        new Vector3(indexWidth * 2, _wallOffset, indexHeight * 2); //left wall
                    _placeThis = Instantiate(_data.Wall[0], currentRow.transform);
                    _placeThis.transform.position = new Vector3(indexWidth * 2, _wallOffset, 0); //right wall

                    indexHeight = 0;
                    indexWidth++;
                    rowCount++;
                }
            }
        }

        Vector3 FinishPosition = _placeThis.transform.position;
        Debug.Log("The last Tile has the postion:" + _placeThis.transform.position.ToString());
        _placeThis = Instantiate(_data.Finish, _thisTransform);
        _placeThis.transform.position = FinishPosition + new Vector3(4, 0.1f, 6.1f);
        Debug.Log("Generated Grid: " + _height + "," + _width + "in GameObject: ", gameObject);
    }

    private static GameObject[] ChooseRndArrayTile(SO_WorldData _data, GameObject[] one)
    {
        int rndArray = Random.Range(0, 5);
        switch (rndArray)
        {
            case 0:
                one = _data.prefab_trueTrap;
                break;
            case 1:
                one = _data.prefab_falseTrap;
                break;
            case 2:
                one = _data.prefab_tile;
                break;
            case 3:
                one = _data.prefab_tile;
                break;
            case 4:
                one = _data.prefab_tile;
                break;
            case 5:
                one = _data.prefab_tile;
                break;
        }

        return one;
    }
}