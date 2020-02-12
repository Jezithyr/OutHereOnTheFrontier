using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "EventSystem/Create New Decision")]
public class EventDecision : ScriptableObject
{


    [SerializeField] public string DecisionInternalName = "";
    [SerializeField] public EventCondition condition;
    [SerializeField] protected List<EventEffect> effects = new List<EventEffect>();

    [SerializeField] protected string optionTitle = "";
    [SerializeField] public string flavorText = "";

    public bool wasChosen = false;

    public Event owner;
    delegate void EventEffectDelegate(ScriptableObject callingObject);
    delegate bool ConditionCheckDelegate(ScriptableObject scriptObj);

    private EventEffectDelegate effectDelegate;
    private ConditionCheckDelegate EventConditionDelegate;

    private void dummyMethod(ScriptableObject temp)
    {

    }


    private bool dummyCondition(ScriptableObject scriptObj)
    {
        return true;
    }

    private void OnEnable()
    {
        foreach (EventEffect effect in effects)
        {
            effectDelegate += effect.Run;
        }
        if (effects.Count == 0)
        {
            effectDelegate += dummyMethod;
        }

        if (condition == null)
        {
            EventConditionDelegate = dummyCondition;
        } else
        {
            EventConditionDelegate = condition.ConditionCheck;
        }
    }



    public bool canSelect()
    {
       return EventConditionDelegate(owner);
    }

    public bool CheckConditions()
    {

        return false;
    }

    public void TriggerDecision()
    {
        wasChosen = true;
        effectDelegate(owner);
    }

}
