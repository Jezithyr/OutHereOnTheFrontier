using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "GameFramework/Conditions/Boolean Condition")]
public class booleanCondition : ConditionScript
{
    [SerializeField] private bool condition;
    public override bool ConditionCheck(ScriptableObject callingObject)
    {
        return condition;
    }



}
