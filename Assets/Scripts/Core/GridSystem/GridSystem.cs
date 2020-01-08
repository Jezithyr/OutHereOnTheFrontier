using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName= "Core/GridSystem/GridController")]
public class GridSystem : ScriptableObject
{
    [SerializeField] private GameManager gameManager;

    [SerializeField] private Vector3 CenterPos;
    [SerializeField] private float CellSize = 1;
    [SerializeField] private int GridXSize = 10;
    [SerializeField] private int GridYSize = 10;

    private Vector3 _originPos;
    private float _zOffset;


    private GridCell[][] _gridArray;
    public GridCell[][] cellArray { get => _gridArray;}


    private void Awake()
    {
        InitalizeGrid();
    }



    public GridCell GetCellAt(Vector2Int gridPos)
    {
        return cellArray[gridPos.x][gridPos.y];
    }

    public GridCell GetCellAtWorldPos(Vector3 worldPos)
    {
        Vector2Int gridPos = ConvertWorldToGridPos(worldPos);
        if (gridPos.x >=0 || gridPos.y >= 0)
        {
            return GetCellAt(gridPos);
        } else {
            return null;
        }
    }

    public Vector2Int ConvertWorldToGridPos(Vector3 worldPos)
    {
        Vector3 OffsetWorldPos = worldPos + _originPos;
        
        int adjustedX = (int)Mathf.Floor((OffsetWorldPos.x - (OffsetWorldPos.x % CellSize))/CellSize);
        int adjustedY = (int)Mathf.Floor((OffsetWorldPos.z - (OffsetWorldPos.z % CellSize))/CellSize);

        return new Vector2Int(adjustedX,adjustedY);        
    }

    public Vector3 ConvertGridToWorldPos(Vector2Int gridPos)
    {

       float outX =  _originPos.x + (gridPos.x*CellSize) + (0.5f*CellSize);
       float outY =  _originPos.y + (gridPos.y*CellSize) + (0.5f*CellSize);
        return new Vector3(outX,outY,_zOffset);
    }



    private void InitalizeGrid()
    {
        _originPos = new Vector3(CenterPos.x - (GridXSize/2) + (0.5f*CellSize),CenterPos.y - (GridYSize/2) + (0.5f*CellSize),CenterPos.z);
        _zOffset = CenterPos.z;
        _gridArray = new GridCell[GridXSize][];

        for (int curCol = 0; curCol < GridXSize; curCol++)
        {
            GridCell[] tempCol = new GridCell[GridYSize];

            for (int curRow = 0; curRow < GridYSize; curRow++)
            {
                GridCell newCell = ScriptableObject.CreateInstance<GridCell>();
                Vector2Int cellGridPos = new Vector2Int(curCol,curRow);
                newCell.Initalize(cellGridPos,ConvertGridToWorldPos(cellGridPos));
                tempCol[curRow] = newCell;
            }
            _gridArray[curCol] = tempCol;
        }
    }


    private void DebugDump()
    {
        int col =0;
        foreach (GridCell[] colArray in _gridArray)
        {
            int row = 0;
            foreach (GridCell curCell in colArray)
            {
                Debug.Log("Cell at: "+col+","+row+" has world coords:  "+curCell.worldPos);
                row++;
            }
            col++;
        }



    }


}
