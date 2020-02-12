using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EventSystem/Conditions/Require Building")]
public class BuildingPrereq : EventCondition
{
    [SerializeField] private Building buildingType;

    public override bool ConditionCheck(ScriptableObject callingObject)
    {
        return buildingType.IsPlaced();
    }
}
