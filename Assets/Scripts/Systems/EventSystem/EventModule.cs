using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "GameFramework/SubSystems/EventModule")]
public class EventModule : Module
{   
    [SerializeField] private List<Event> activeEvents = new List<Event>() ;





    public override void Initialize()
    {
        
            //initalizion

    }
    
    public override void Update()
    {
        
        // on tick

    }
}
