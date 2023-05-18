using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WorldGeneration_Script : MonoBehaviour
{
    [SerializeField] SO_WorldData _data;
    Grid_World _worldGrid;
    private int _height = 3;
    [SerializeField] private int _width = 3;
    Transform _thisTransform;
    GameObject _placeThis;
    // Start is called before the first frame update
    void Start()
    {
        _worldGrid = GetComponent<Grid_World>();
        if (_worldGrid == null) { _worldGrid = new Grid_World(_height, _width); }
        _thisTransform = GetComponent<Transform>();
        this.transform.position = new Vector3(0, 0, 0);
        GenerateGrid(_worldGrid, _data);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void GenerateGrid(Grid_World _worldGrid, SO_WorldData _data)
    {
        Vector2Int[,] _gridMap = _worldGrid.GetPlacementGrid;
        int indexHeight = 0;
        int indexWidth = 0;

        if (_worldGrid == null)
        { Debug.Log("There were no grid"); }

        foreach (var item in _gridMap)
        {
            if (indexHeight <= 2)
            {
                int rndIndex = Random.Range(0, _data.prefab_trueTrap.Length);
                _placeThis = Instantiate(_data.prefab_tile[rndIndex]);
                if (rndIndex == 0) { _placeThis.transform.position = new Vector3(indexWidth * 2, 0, indexHeight * 2); }
                if (rndIndex == 2) { _placeThis.transform.position = new Vector3(indexWidth * -2, 0, indexHeight * -2); }

                indexHeight++;
                if (indexHeight == 3)
                {
                    indexHeight = 0;
                    indexWidth++;
                }
            }
        }
        Debug.Log("Generated Grid: " + _height + "," + _width + "in GameObject: ", gameObject);
    }
}
