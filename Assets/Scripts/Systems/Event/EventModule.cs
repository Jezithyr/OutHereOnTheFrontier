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


    ScriptedUIBehavior EventUI;
    GameObject EventUIObject;


    public void ButtonClick(int choiceIndex)
    {

        if (activeEvent.choices.Count > choiceIndex)
        {
            activeEvent.choices[choiceIndex].TriggerDecision();
            HideUI();
        }
        
    }

    public void InitializePrefab()
    {
        EventUIObject = GameObject.Instantiate(linkedEventPrefab);
        EventUI = EventUIObject.GetComponent<ScriptedUIBehavior>();
        HideUI();
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
        if (activeEvent == null) return;
        EventUI.GetElementByName("EventTitle").GetComponentInChildren<TextMeshProUGUI>().SetText(activeEvent.eventTitle);
        EventUI.GetElementByName("FlavorText").GetComponentInChildren<TextMeshProUGUI>().SetText(activeEvent.flavorText);
        if (activeEvent.choices.Count > 0) 
        {
            EventUI.GetElementByName("Choice1").gameObject.SetActive(true);
            EventUI.GetElementByName("Choice1").GetComponentInChildren<TextMeshProUGUI>().SetText("  "+ activeEvent.choices[0].flavorText);
        }
        else 
        {
            EventUI.GetElementByName("Choice1").gameObject.SetActive(false);
        }

        if (activeEvent.choices.Count > 1) 
        {
            EventUI.GetElementByName("Choice2").gameObject.SetActive(true);
            EventUI.GetElementByName("Choice2").GetComponentInChildren<TextMeshProUGUI>().SetText("  "+ activeEvent.choices[1].flavorText);
        }
        else 
        {
            EventUI.GetElementByName("Choice2").gameObject.SetActive(false);
        }

        if (activeEvent.choices.Count > 2) 
        {
            EventUI.GetElementByName("Choice3").gameObject.SetActive(true);
            EventUI.GetElementByName("Choice3").GetComponentInChildren<TextMeshProUGUI>().SetText("  "+ activeEvent.choices[2].flavorText);
        }
        else 
        {
            EventUI.GetElementByName("Choice3").gameObject.SetActive(false);
        }
        
        if (activeEvent.choices.Count > 3) 
        {
            EventUI.GetElementByName("Choice4").gameObject.SetActive(true);
            EventUI.GetElementByName("Choice4").GetComponentInChildren<TextMeshProUGUI>().SetText("  "+ activeEvent.choices[3].flavorText);
        }
        else 
        {
            EventUI.GetElementByName("Choice4").gameObject.SetActive(false);
        }
    }


    public void showEvent(int eventIndex)
    {
        setActiveEvent(eventIndex);
        UpdateUI();
        ShowUI();
    }


    public void setActiveEvent(int eventIndex)
    {
        activeEvent = activeEvents[eventIndex]; 
    }

    public override void Start()
    {

        if (SceneManager.GetActiveScene() != gameplayScene) return;
        //activeEvent = activeEvents[0];
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
