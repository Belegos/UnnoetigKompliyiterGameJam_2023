using UnityEngine;

public class WorldGeneration_Script : MonoBehaviour
{
    [SerializeField] SO_WorldData _data;
    [SerializeField] private int _height;
    [SerializeField] private int _width;
    private int _maxWidth;
    private int _maxHeight;
    int rowCount = 1;
    private int _trapCounter = 0;
    private int _falseTrapCounter = 0;
    private int _floorTileCounter = 0;
    private int _roofTileCount = 0;
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
        _placeThis.transform.position = new Vector3(-2, 0, 0);
        _placeThis.transform.name = "001_Start";

        Vector2Int[,] _gridMap = _worldGrid.GetPlacementGrid;
        int indexHeight = 0;
        int indexWidth = 0;

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
                    //floortile 1
                    one = ChooseRndArrayTile(_data, one);
                    rndIndex = Random.Range(0, one.Length);
                    _placeThis = Instantiate(one[rndIndex], currentRow.transform);
                    NameFloorAndTraps(_data, one);
                    _placeThis.transform.position = new Vector3(indexWidth * 2, 0, indexHeight + offSetZTileOne);

                    //roof 1
                    _placeThis = Instantiate(_data.Wall[1], currentRow.transform);
                    _placeThis.transform.position = new Vector3(indexWidth * 2, 3, indexHeight + offSetZTileOne);
                    _placeThis.transform.name = _roofTileCount + "_Roof_Row_" + rowCount;
                }

                if (indexHeight == 1)
                {
                    //floortile2
                    one = ChooseRndArrayTile(_data, one);
                    rndIndex = Random.Range(0, one.Length);
                    NameFloorAndTraps(_data, one);
                    _placeThis = Instantiate(one[rndIndex], currentRow.transform);
                    _placeThis.transform.position = new Vector3(indexWidth * 2, 0, indexHeight + offSetZTileTwo);

                    //roof 2
                    _placeThis = Instantiate(_data.Wall[1], currentRow.transform);
                    _placeThis.transform.position = new Vector3(indexWidth * 2, 3, indexHeight + offSetZTileTwo);
                    _placeThis.transform.name = _roofTileCount + "_Roof_Row_" + rowCount;
                }


                if (indexHeight == 2)
                {
                    //floortile 3
                    one = ChooseRndArrayTile(_data, one);
                    rndIndex = Random.Range(0, one.Length);
                    _placeThis = Instantiate(one[rndIndex], currentRow.transform);
                    NameFloorAndTraps(_data, one);
                    _placeThis.transform.position = new Vector3(indexWidth * 2, 0, indexHeight + offSetZTileThree);

                    //roof 3
                    _placeThis = Instantiate(_data.Wall[1], currentRow.transform);
                    _placeThis.transform.position = new Vector3(indexWidth * 2, 3, indexHeight + offSetZTileThree);
                    _placeThis.transform.name = _roofTileCount + "_Roof_Row_" + rowCount;
                }

                indexHeight++;
                if (indexHeight == _maxHeight)
                {
                    _placeThis = Instantiate(_data.Wall[0], currentRow.transform);
                    //NameFloorAndTraps(_data, one);
                    _placeThis.transform.position =
                        new Vector3(indexWidth * 2, _wallOffset, indexHeight * 2); //left wall
                    _placeThis.transform.name = "Wall_Row_" + rowCount;
                    _placeThis = Instantiate(_data.Wall[0], currentRow.transform);
                    _placeThis.transform.position = new Vector3(indexWidth * 2, _wallOffset, 0); //right wall
                    _placeThis.transform.name = "Wall_Row_" + rowCount;

                    indexHeight = 0;
                    indexWidth++;
                    rowCount++;
                }
            }
        }

        Vector3 FinishPosition = _placeThis.transform.position;
        _placeThis = Instantiate(_data.Finish, _thisTransform);
        _placeThis.transform.position = FinishPosition + new Vector3(4, 0.1f, 6.1f);
        Debug.Log("Generated Grid: " + _height + "," + _width + "in GameObject: ", gameObject);
        Debug.Log("Generated " + _floorTileCounter + " normal Floortiles.");
        Debug.Log("Generated " + _trapCounter + " normal Traptiles.");
        Debug.Log("Generated " + _falseTrapCounter + " false Traptiles.");
    }

    private void NameFloorAndTraps(SO_WorldData _data, GameObject[] one)
    {
        if (one == _data.prefab_trueTrap)
        {
            _placeThis.transform.name = _trapCounter + "_Trap_Row_" + rowCount;
            _trapCounter++;
        }

        if (one == _data.prefab_falseTrap)
        {
            _placeThis.transform.name = _falseTrapCounter + "_FalseTrap_" + rowCount;
            _falseTrapCounter++;
        }

        if (one == _data.prefab_tile)
        {
            _placeThis.transform.name = _floorTileCounter + "_Floor_" + rowCount;
            _floorTileCounter++;
        }
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
                one = _data.prefab_trueTrap;
                break;
            case 4:
                one = _data.prefab_tile;
                break;
            case 5:
                one = _data.prefab_falseTrap;
                break;
        }

        return one;
    }
}