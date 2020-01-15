using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;


[CreateAssetMenu(menuName = "GameFramework/SubSystems/EventModule")]
public class EventModule : Module
{   
    [SerializeField] private List<Event> activeEvents = new List<Event>() ;

    [SerializeField] private GameObject linkedEventUI;
    [SerializeField] private TextMeshProUGUI eventUITitle;
    [SerializeField] private TextMeshProUGUI eventUIFlavor;

    [SerializeField] private List<Button> buttonList = new List<Button>();

    Event activeEvent;




    public void ButtonClick(int choiceIndex)
    {
        activeEvent.choices[choiceIndex].TriggerDecision();
    }






    public void UpdateUI()
    {
        eventUITitle.text = activeEvent.eventTitle;
        eventUIFlavor.text = activeEvent.flavorText;
        
        int index = 0;
        foreach (EventDecision choice in activeEvent.choices)
        {
            if (!choice.condition.ConditionCheck(this))
            {
                buttonList[index].gameObject.SetActive(false);
            }
            else
            {
                buttonList[index].GetComponent<TextMeshProUGUI>().text = choice.flavorText;
            }
            index++;
        }
    }





    public override void Initialize()
    {
        activeEvent = activeEvents[0];

    }
    
    public override void Update()
    {
      //  foreach (Event newEvent in activeEvents)
       // {
        //    if (newEvent.triggerCondition.ConditionCheck(newEvent))
        //    {
        //        activeEvent = newEvent;
        //    }
       // }

    }
}
