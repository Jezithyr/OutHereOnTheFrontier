using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameFramework/Conditions/Time Condition")]
public class TimeCondition : ConditionScript
{
    [SerializeField] private PlayingState gameState;
    [SerializeField] private int timeToTrigger;
    public override bool ConditionCheck(ScriptableObject callingObject)
    {
        return (gameState.GameTimer == timeToTrigger);
    }
}
