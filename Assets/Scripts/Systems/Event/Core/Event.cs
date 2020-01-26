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

    private Dictionary<string, EventDecision> menuOptions = new Dictionary<string, EventDecision>();

    private void OnEnable()
    {
        menuOptions.Clear();

        if (choices.Count != 0)
        {
            foreach (EventDecision decision in choices)
            {
                menuOptions.Add(decision.DecisionInternalName, decision);
            }
        }
    }

    public EventDecision GetDecision(string decisionName)
    {
        return menuOptions[decisionName];
    }


}
