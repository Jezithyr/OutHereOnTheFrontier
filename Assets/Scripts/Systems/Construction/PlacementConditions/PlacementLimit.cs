using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BuildingSystem/Placement Conditions/PlacementLimit")]
public class PlacementLimit : PlacementCondition
{
    [SerializeField] private short limit ;
     public override bool ConditionCheck(GameObject prefab, Building buildingData)
    {
        Debug.Log(buildingData.GetInstanceCount());
        return buildingData.GetInstanceCount() <= limit-1;
    }
}
