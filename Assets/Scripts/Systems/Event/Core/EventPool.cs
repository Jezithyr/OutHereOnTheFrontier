using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EventSystem/Create Event Pool")]
public class EventPool : ScriptableObject
{
    
    //TODO add checking for event conditions after the event has been selected

    [SerializeField] private bool selectRandomEvent = true;
    [SerializeField] private List<Event> EventList = new List<Event>();
    public List<Event> ListAll{get=>EventList;}

    public Event GetRandomEvent()
    {
        return EventList[Random.Range(0,EventList.Count-1)];
    }

    public Event CheckEventConditions()
    {
        foreach (Event chkEvent in EventList)
        {
            if (chkEvent.triggerCondition.Count <= 0 && chkEvent.CheckConditions(chkEvent))
            {
                return chkEvent;
            }
        }
        return null;
    }

}
