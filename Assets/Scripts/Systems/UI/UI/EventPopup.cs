using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(menuName = "UISystem/UI/Create New Event Popup")]
public class EventPopup : ScriptedUI
{
    private void OnEnable()
    {
        ClearBehaviors(); //this needs to be present in every ui otherwise unity serialization breaks everything
    }





}
