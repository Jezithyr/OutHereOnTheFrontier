using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "EventSystem/Conditions/Require Event Shown")]
public class EventPrerequisite : ConditionScript
{
    [SerializeField] private Event PreReqEvent;
    public override bool ConditionCheck(ScriptableObject callingObject)
    {
        return PreReqEvent.Completed;
    }
}
