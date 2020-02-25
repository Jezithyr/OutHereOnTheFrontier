using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EventSystem/Conditions/Require Event Choice")]
public class EventChoicePreReq : ConditionScript
{
    [SerializeField] private EventDecision choiceToCheck;

    public override bool ConditionCheck(ScriptableObject callingObject)
    {
        return choiceToCheck.wasChosen;
    }
}
