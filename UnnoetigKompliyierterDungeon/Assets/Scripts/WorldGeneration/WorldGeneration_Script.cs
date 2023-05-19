using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WorldGeneration_Script : MonoBehaviour
{
    [SerializeField] SO_WorldData _data;
    [SerializeField]private int _height;
    [SerializeField]private int _width;
    [SerializeField]private float _wallOffset = -0.1f;
    Grid_World _worldGrid;
    private Transform _thisTransform;
    private GameObject _placeThis;

    void Start()
    {
        _worldGrid = new Grid_World(_height, _width);
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

        for (int i = 0; i < _width; i++) // y achse
        {
            for (int j = 0; j < _height; j++) // x achse
            {
                int rndIndex = Random.Range(0, _data.prefab_trueTrap.Length);
                _placeThis = Instantiate(_data.prefab_tile[rndIndex], _thisTransform);
                if (rndIndex == 0)
                {
                    _placeThis.transform.position = new Vector3(indexWidth * 2, 0, indexHeight * 2);
                }
                if (rndIndex == 2)
                {
                    _placeThis.transform.position = new Vector3(indexWidth * -2, 0, indexHeight * -2);
                }

                indexHeight++;
                if (indexHeight == 3)
                {
                    _placeThis = Instantiate(_data.Wall[0]);
                    _placeThis.transform.position = new Vector3(indexWidth * 2, _wallOffset, indexHeight * 2);//left wall
                    _placeThis = Instantiate(_data.Wall[0]);
                    _placeThis.transform.position = new Vector3(indexWidth * 2, _wallOffset, 0);//right wall
                    indexHeight = 0;
                    indexWidth++;
                }
            }
        }
        Debug.Log("Generated Grid: " + _height + "," + _width + "in GameObject: ", gameObject);
    }
}