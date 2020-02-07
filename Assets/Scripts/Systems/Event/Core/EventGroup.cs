using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EventSystem/Create Event Group")]
public class EventGroup : ScriptableObject
{
    [SerializeField] private float TimeToTrigger;
    public float TriggerTime{get=>TimeToTrigger;}

    [SerializeField] private List<Event> EventList;
    public List<Event> ListAll{get=>EventList;}

    public Event GetRandomEvent()
    {
        return EventList[Random.Range(0,EventList.Count-1)];
    }

}
