using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "BuildingSystem/Placement Conditions/Minimum Distance From Others")]
public class MinimumDistanceFromOthers : PlacementCondition
{
    [SerializeField] private LayerMask Layer;//building layer
    [SerializeField] private float distance = 10;

    public override bool ConditionCheck(GameObject prefab, Building buildingData)
    {
        Collider[] hitColliders = Physics.OverlapSphere(prefab.transform.position, distance);

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
