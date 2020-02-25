﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BuildingSystem/Placement Conditions/PreventOverlap")]
public class PreventOverlap : PlacementCondition
{
    
    [SerializeField] private LayerMask Layer;//building layer

    public override bool ConditionCheck(GameObject prefab, Building buildingData)
    {
        Collider[] hitColliders = Physics.OverlapSphere(prefab.transform.position, buildingData.Radius);

        Debug.Log("Collision Check = "+hitColliders.Length);
        foreach (var collider in hitColliders)
        {
            if (Layer == (Layer | (1 <<  collider.gameObject.layer)))
            {
                return false;
            }
        }
        return true;
    }
}
