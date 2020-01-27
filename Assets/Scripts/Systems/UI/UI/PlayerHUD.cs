using UnityEngine;
using System.Collections.Generic;
using TMPro;



[CreateAssetMenu(menuName = "UISystem/UI/Create New PlayerHud")]
public class PlayerHUD : ScriptedUI
{


    [SerializeField] private ResourceModule resourceSystem;

    [SerializeField] private List<Resource>  ResourceList = new List<Resource>();

    [SerializeField] private List<string> ResourceDisplayNames = new List<string>();

    private List<TextMeshProUGUI> linkedResourceDisplays = new List<TextMeshProUGUI>();

    private void OnEnable()
    {
        ClearBehaviors(); //this needs to be present in every ui otherwise unity serialization breaks everything
        linkedResourceDisplays.Clear(); //prevents a bug with hotreloading
    }

    public override void Start()
    {
        
        for (int i = 0; i < ResourceDisplayNames.Count; i++)
        {

            linkedResourceDisplays.Add(linkedUI.GetElementByName(ResourceDisplayNames[i]).GetComponentInChildren<TextMeshProUGUI>());
        }

    }

    public override void Update()
    {
        
        for (int i = 0; i < linkedResourceDisplays.Count; i++)
        {
            linkedResourceDisplays[i].SetText(resourceSystem.GetResourceStorage(ResourceList[i])+ "/"+ resourceSystem.GetResourceLimit(ResourceList[i]));
        }
    }

}
