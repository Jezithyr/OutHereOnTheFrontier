using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBuildingTest : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    private void Start()
    {
        ConstructionManager buildingSys = gameManager.ConstructionSubSystem;
        buildingSys.CreateBuildingAt(new Vector2Int(0,0),new Quaternion(),buildingSys.enabledBuildings[0]);
    }
}
