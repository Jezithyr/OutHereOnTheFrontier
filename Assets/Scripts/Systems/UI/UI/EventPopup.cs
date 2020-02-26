using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;



[CreateAssetMenu(menuName = "UISystem/UI/Create New Event Popup")]
public class EventPopup : ScriptedUI
{
    [SerializeField] private Color disabledButtonColor;

    private List<GameObject> choiceButtons = new List<GameObject>();
    private TextMeshProUGUI eventFlavorText;
    private TextMeshProUGUI eventTitleText;
    private TextMeshProUGUI effectTitleText;
    private TextMeshProUGUI effectFlavorText;
    private TextMeshProUGUI effectButtonText;
    private Event linkedEvent;

    private void OnEnable()
    {
        ClearBehaviors(); //this needs to be present in every ui otherwise unity serialization breaks everything    
        choiceButtons.Clear();
    }


    public override void Start()
    {
        choiceButtons.Add(linkedUI.GetElementByName("Choice1"));
        choiceButtons.Add(linkedUI.GetElementByName("Choice2"));
        choiceButtons.Add(linkedUI.GetElementByName("Choice3"));
        choiceButtons.Add(linkedUI.GetElementByName("Choice4"));

        foreach (var choiceButton in choiceButtons)
        {
            Debug.Log(choiceButton.transform.parent);
            choiceButton.GetComponentInChildren<Button>().interactable = false;
        }


        eventTitleText =  linkedUI.GetElementByName("EventTitle").GetComponent<TextMeshProUGUI>();
        eventFlavorText =  linkedUI.GetElementByName("EventFlavorText").GetComponent<TextMeshProUGUI>();
        effectTitleText =  linkedUI.GetElementByName("EffectTitle").GetComponent<TextMeshProUGUI>();
        effectFlavorText =  linkedUI.GetElementByName("EffectFlavorText").GetComponent<TextMeshProUGUI>();
        effectButtonText = linkedUI.GetElementByName("EffectButtonText").GetComponent<TextMeshProUGUI>();
    }

    public void PressChoice(int choiceNum)
    {
        if (choiceNum > 3) return;
        LoadChoiceData(choiceNum);
        ShowEffectPanel();
    }

    public void ShowEffectPanel()
    {
        linkedUI.GetElementByName("EventPanel").SetActive(false);
        linkedUI.GetElementByName("EffectPanel").SetActive(true);
    }


    private void LoadChoiceData(int choiceIndex)
    {
        effectTitleText.SetText(linkedEvent.choices[choiceIndex].effectTitle);
        effectFlavorText.SetText(linkedEvent.choices[choiceIndex].effectText);
        effectButtonText.SetText(linkedEvent.choices[choiceIndex].effectButtonText);
    }

    public void LoadEventData(Event eventData)
    {

        int eventCount = eventData.choices.Count;
        int eventDiff = 4 - eventCount; 

        for (int i = 0; i < eventCount; i++)
        {
            choiceButtons[i].GetComponentInChildren<TextMeshProUGUI>().SetText( eventData.choices[i].flavorText);
            
            if (eventData.choices[i].CheckConditions())
            {
               choiceButtons[i].GetComponentInChildren<Button>().interactable = true; 
            }

           
        }

        if (eventDiff > 0)
        {
            for (int i = 3; i > eventCount-1; i--)
            {
                choiceButtons[i].SetActive(false);
            }       
        }

        linkedEvent = eventData;
        eventFlavorText.SetText(eventData.flavorText);
        eventTitleText.SetText(eventData.eventTitle);

        // effectTitleText.SetText(eventData.effectTitle);
        // effectFlavorText.SetText(eventData.effectText);
        // effectButtonText.SetText(eventData.effectButtonText);
    }


    public void cleanup()
    {
        ClearBehaviors(); //this needs to be present in every ui otherwise unity serialization breaks everything    
        choiceButtons.Clear();
    }

    public void Reset()
    {
        
        linkedUI.GetElementByName("EventPanel").SetActive(true);
        linkedUI.GetElementByName("EffectPanel").SetActive(false);
    }
}
