using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlacementCondition : ScriptableObject
{
    public abstract bool ConditionCheck(GameObject prefab, Building buildingData);
}
