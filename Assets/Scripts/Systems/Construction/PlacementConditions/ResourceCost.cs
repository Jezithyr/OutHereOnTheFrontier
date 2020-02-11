using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BuildingSystem/Placement Conditions/Check Resource")]
public class ResourceCost : PlacementCondition
{

    
    [SerializeField] private ResourceModule resourceController;


    

    public override bool ConditionCheck(GameObject prefab, Building buildingData)
    {

        bool enoughResources = true;
        for (int i = 0; i < buildingData.ResourceForCost.Count; i++)
        {
            enoughResources = enoughResources & resourceController.GetResourceStorage(buildingData.ResourceForCost[i]) >= buildingData.AmountForCost[i];
        }
        if (!enoughResources) return false;

        for (int i = 0; i < buildingData.ResourceForCost.Count; i++)
        {
            resourceController.RemoveResource(buildingData.ResourceForCost[i],buildingData.AmountForCost[i]);
        }
        return true;
    }
}
