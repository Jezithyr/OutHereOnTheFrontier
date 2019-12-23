using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCell : ScriptableObject
{
    private bool _pathable;
    public bool IsPathable{get => _pathable;}

    private Vector2Int _gridPos;
    public Vector2Int gridPos{get =>_gridPos;}
    
    private Vector3 _worldPos;
    public Vector3 worldPos{get =>_worldPos;}



    private List<GameObject> _containedObjects;
    public List<GameObject> ContainedObjects{get =>_containedObjects;}


    public void Initalize(Vector2Int gridPos, Vector3 worldPos)
    {
        _gridPos = gridPos;
        _worldPos = worldPos;
    }
}
