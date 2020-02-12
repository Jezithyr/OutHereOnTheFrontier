using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "EventSystem/Create New Event")]
public class Event : ScriptableObject
{
    [SerializeField] public string EventInternalName = "";
    [SerializeField] public bool isActive;
    [SerializeField] public ConditionScript triggerCondition;
    [SerializeField] public List<EventDecision> choices = new List<EventDecision>();

    [SerializeField] public string eventTitle = "";
    [TextArea] public string flavorText = "";

    private bool IsComplete = false;
    public bool Completed{get =>IsComplete;}

    private Dictionary<string, EventDecision> menuOptions = new Dictionary<string, EventDecision>();

    bool MakeDecision(int DecisionIndex)
    {
        if (DecisionIndex >= choices.Count) return false;
        if ( choices[DecisionIndex].canSelect())
        {
            IsComplete = true;
            choices[DecisionIndex].TriggerDecision();
            return true;
        }
        return false;
    }

    private void OnEnable()
    {
        menuOptions.Clear();

        if (choices.Count != 0)
        {
            foreach (EventDecision decision in choices)
            {
                menuOptions.Add(decision.name, decision);
            }
        }
    }

    public EventDecision GetDecision(string decisionName)
    {
        return menuOptions[decisionName];
    }


}
