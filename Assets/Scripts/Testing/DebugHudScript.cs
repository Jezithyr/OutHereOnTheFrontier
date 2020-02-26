using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


[CreateAssetMenu(menuName = "UISystem/HUD/Create New Debug Hud")]
public class DebugHudScript : ScriptedUI
{  
    [SerializeField] private ResourceModule resourceSystem;
    [SerializeField] private Resource WoodResource;
    [SerializeField] private Resource MetalResource;
    [SerializeField] private Resource AlloyResource;

    private TextMeshProUGUI metalDisplay;
    private TextMeshProUGUI woodDisplay;
    private TextMeshProUGUI alloyDisplay;

    public override void Update()
    {
        //Debug.Log("HUD UPDATE " + resourceSystem.GetResourceStorage(MetalResource));
        metalDisplay = linkedBehaviorScripts[0].GetElementByName("MetalDisplay").GetComponent<TextMeshProUGUI>();
        woodDisplay = linkedBehaviorScripts[0].GetElementByName("WoodDisplay").GetComponent<TextMeshProUGUI>();
        alloyDisplay = linkedBehaviorScripts[0].GetElementByName("AlloyDisplay").GetComponent<TextMeshProUGUI>();

        metalDisplay.SetText((resourceSystem.GetResourceStorage(MetalResource)+ "/"+ resourceSystem.GetResourceLimit(MetalResource)));
        woodDisplay.SetText((resourceSystem.GetResourceStorage(WoodResource)+ "/"+ resourceSystem.GetResourceLimit(WoodResource)));
        //metalDisplay.text = resourceSystem.GetResourceStorage(AlloyResource)+ "/"+ resourceSystem.GetResourceLimit(AlloyResource);


    }
}
