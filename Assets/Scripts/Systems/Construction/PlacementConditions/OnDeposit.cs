using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "BuildingSystem/Placement Conditions/On Resource Deposit")]
public class OnDeposit : PlacementCondition
{
    [SerializeField] private LayerMask Layer;//building layer
    public override bool ConditionCheck(GameObject prefab, Building buildingData)
    {
        RaycastHit hit;
        if (Physics.Raycast(prefab.transform.position, prefab.transform.TransformDirection(Vector3.down), out hit, 10f, Layer))
        {
            return true;
        }
        return false;
    }
}
