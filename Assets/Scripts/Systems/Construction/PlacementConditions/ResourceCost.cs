using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BuildingSystem/Placement Conditions/Check Resource")]
public class ResourceCost : PlacementCondition
{

    
    [SerializeField] private ResourceModule resourceController;

    [SerializeField] private List<Resource> ResourceForCost;
    [SerializeField] private List<int> AmountForCost;
    

    public override bool ConditionCheck(GameObject prefab, Building buildingData)
    {

        bool enoughResources = true;
        for (int i = 0; i < ResourceForCost.Count; i++)
        {
            enoughResources = enoughResources & resourceController.GetResourceStorage(ResourceForCost[i]) >= AmountForCost[i];
        }
        if (!enoughResources) return false;

        for (int i = 0; i < ResourceForCost.Count; i++)
        {
            resourceController.RemoveResource(ResourceForCost[i],AmountForCost[i]);
        }
        return true;
    }
}
