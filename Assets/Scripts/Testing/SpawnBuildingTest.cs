using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBuildingTest : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    private void Start()
    {
        ConstructionManager buildingSys = gameManager.ConstructionSubSystem;
        buildingSys.CreateBuildingAtGridPos(new Vector2Int(25,25),new Quaternion(),buildingSys.enabledBuildings[0]);
    }
}
