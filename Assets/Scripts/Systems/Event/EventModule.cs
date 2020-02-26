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

    [SerializeField] private ScriptedUI eventUI;

    [SerializeField] private PlayingState playState;

    [SerializeField] private Scene gameplayScene;

    [SerializeField] private EventPool globalEventPool;//these can be triggered any time by conditions

    [SerializeField] private List<EventPool> EventPools = new List<EventPool>(); //this are random event pools

    [SerializeField] private List<int> PoolTimes = new List<int>(); //the times that random events can be triggered



    Event activeEvent;
    private int eventUIID;
    //ScriptedUIBehavior EventUI;
    //GameObject EventUIObject;


    public void ButtonClick(int choiceIndex)
    {

        if (activeEvent.choices.Count > choiceIndex)
        {
            ((EventPopup)eventUI).PressChoice(choiceIndex);
            activeEvent.choices[choiceIndex].TriggerDecision();
        }
    }

    public void ClosePopUp()
    {
        ((EventPopup)eventUI).Reset();
        HideUI();
        playState.UnPauseGame();
        playState.ElapsedTime += 1;
        playState.GameTimer -= 1;
    }


    // public void InitializePrefab()
    // {
    //    eventUIID = eventUI.CreateUIInstance();
    //    eventUI.ToggleUI(eventUIID, false);
    // }

    public void ShowUI()
    {
        eventUI.ToggleUI(eventUIID, true);
        // EventUIObject.SetActive(true);
    }

    public void HideUI()
    {
        eventUI.ToggleUI(eventUIID, false);
        // EventUIObject.SetActive(false);
    }


    //todo change this
    public void UpdateUI()
    {

        // if (activeEvent == null) return;
        // EventUI.GetElementByName("EventTitle").GetComponentInChildren<TextMeshProUGUI>().SetText(activeEvent.eventTitle);
        // EventUI.GetElementByName("FlavorText").GetComponentInChildren<TextMeshProUGUI>().SetText(activeEvent.flavorText);
        // if (activeEvent.choices.Count > 0) 
        // {
        //     EventUI.GetElementByName("Choice1").gameObject.SetActive(true);
        //     EventUI.GetElementByName("Choice1").GetComponentInChildren<TextMeshProUGUI>().SetText("  "+ activeEvent.choices[0].flavorText);
        // }
        // else 
        // {
        //     EventUI.GetElementByName("Choice1").gameObject.SetActive(false);
        // }

        // if (activeEvent.choices.Count > 1) 
        // {
        //     EventUI.GetElementByName("Choice2").gameObject.SetActive(true);
        //     EventUI.GetElementByName("Choice2").GetComponentInChildren<TextMeshProUGUI>().SetText("  "+ activeEvent.choices[1].flavorText);
        // }
        // else 
        // {
        //     EventUI.GetElementByName("Choice2").gameObject.SetActive(false);
        // }

        // if (activeEvent.choices.Count > 2) 
        // {
        //     EventUI.GetElementByName("Choice3").gameObject.SetActive(true);
        //     EventUI.GetElementByName("Choice3").GetComponentInChildren<TextMeshProUGUI>().SetText("  "+ activeEvent.choices[2].flavorText);
        // }
        // else 
        // {
        //     EventUI.GetElementByName("Choice3").gameObject.SetActive(false);
        // }
        
        // if (activeEvent.choices.Count > 3) 
        // {
        //     EventUI.GetElementByName("Choice4").gameObject.SetActive(true);
        //     EventUI.GetElementByName("Choice4").GetComponentInChildren<TextMeshProUGUI>().SetText("  "+ activeEvent.choices[3].flavorText);
        // }
        // else 
        // {
        //     EventUI.GetElementByName("Choice4").gameObject.SetActive(false);
        // }
    }


    public void showEvent(int eventIndex)
    {
        setActiveEventFromIndex(eventIndex);
        UpdateUI();
        ShowUI();
    }

    private void showEvent(Event eventToShow)
    {
        activeEvent = eventToShow;
        ((EventPopup)eventUI).LoadEventData(activeEvent);
        playState.PauseGame();
        UpdateUI();
        ShowUI();
    }


    public void setActiveEventFromIndex(int eventIndex)
    {
        activeEvent = activeEvents[eventIndex]; 
        ((EventPopup)eventUI).LoadEventData(activeEvent);
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
        Event globalTempEvent = globalEventPool.CheckEventConditions();
        if (globalTempEvent != null)
        {
            showEvent(globalTempEvent);
            return; //early return
        }

        for (int i = 0; i < EventPools.Count; i++)
        {
           if (playState.ElapsedTime == PoolTimes[i])
            {
                showEvent(EventPools[i].GetRandomEvent());
                return;
            } 
        }
    }

    public override void Reset()
    {
        
    }
}
