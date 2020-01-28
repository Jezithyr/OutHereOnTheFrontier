using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "UISystem/UI/Create New BuildingMenu")]
public class BuildingMenu : ScriptedUI
{
    private void OnEnable()
    {
        ClearBehaviors(); //this needs to be present in every ui otherwise unity serialization breaks everything
    }
    
    public override void Start()
    {
        Debug.Log("Test");
    }

    public override void Update()
    {
        Debug.Log("TestTick");
    }

}
