using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "GameFramework/SubSystems/EventModule")]
public class EventModule : Module
{   
    [SerializeField] private List<Event> activeEvents = new List<Event>() ;

    [SerializeField] private GameObject linkedEventPrefab;

    [SerializeField] private Scene gameplayScene;

    Event activeEvent;


    EventPopupLinks EventUI;
    GameObject EventUIObject;


    public void ButtonClick(int choiceIndex)
    {

        if (activeEvent.choices.Count > choiceIndex)
        {
            activeEvent.choices[choiceIndex].TriggerDecision();
            HideUI();
        }
        
    }



    public void ShowUI()
    {
        EventUIObject.SetActive(true);
    }

    public void HideUI()
    {
        EventUIObject.SetActive(false);
    }

    public void UpdateUI()
    {
        EventUIObject = GameObject.Instantiate(linkedEventPrefab);
        EventUI = EventUIObject.GetComponent<EventPopupLinks>();

        EventUI.Title.text = activeEvent.eventTitle;
        EventUI.Flavor.text = activeEvent.flavorText;
        if (activeEvent.choices.Count > 0) 
        {
            EventUI.C1Button.gameObject.SetActive(true);
            EventUI.C1Button.text = "  "+ activeEvent.choices[0].flavorText;
        }
        else 
        {
            EventUI.C1Button.gameObject.SetActive(false);
        }

        if (activeEvent.choices.Count > 1) 
        {
            EventUI.C2Button.gameObject.SetActive(true);
            EventUI.C2Button.text = "  "+ activeEvent.choices[1].flavorText;
        }
        else 
        {
            EventUI.C2Button.gameObject.SetActive(false);
        }

        if (activeEvent.choices.Count > 2) 
        {
            EventUI.C3Button.gameObject.SetActive(true);
            EventUI.C3Button.text = "  "+ activeEvent.choices[2].flavorText;
        }
        else 
        {
            EventUI.C3Button.gameObject.SetActive(false);
        }
        
        if (activeEvent.choices.Count > 3) 
        {
            EventUI.C4Button.gameObject.SetActive(true);
            EventUI.C4Button.text = "  "+ activeEvent.choices[3].flavorText;
        }
        else 
        {
            EventUI.C4Button.gameObject.SetActive(false);
        }
    }





    public override void Start()
    {

        if (SceneManager.GetActiveScene() != gameplayScene) return;
        activeEvent = activeEvents[0];
        UpdateUI();
        HideUI();
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
