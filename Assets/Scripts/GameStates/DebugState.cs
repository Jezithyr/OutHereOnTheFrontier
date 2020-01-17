using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DebugState : GameState
{ 
    [SerializeField]
    private CameraModule camModule;
    
    [SerializeField] private UIModule uiModule;
    [SerializeField] private ScriptedUI playerHUD;
    
    [SerializeField]
    private ScriptedCamera customCamera ;

    [SerializeField] private ModuleManager moduleManager;
    [SerializeField] private EventModule eventModule ;
    [SerializeField] private ConstructionModule buildingModule;
    [SerializeField] private Building Producer;
    [SerializeField] private Building Consumer;
    private Vector3 targetTranslation;

    private Camera cam;
    private FreeOrbitCam activeCam;




    private int playerHudId;

    public override void OnActivate(GameState lastState)
    {
        Debug.Log("Entered Debug State");
        
        ScriptedCamera newCam = camModule.AddScriptedCameraInstance(customCamera);
        activeCam = (FreeOrbitCam)newCam;


        playerHudId = uiModule.CreateInstance(playerHUD);
        Debug.Log("PlayerHUD UI id: "+ playerHudId);
        uiModule.Show(playerHUD,playerHudId);
    }
    public override void OnDeactivate(GameState newState)
    {

    }

    public override void OnUpdate()
    {
        targetTranslation = activeCam.targetPosition;
        if (Input.GetButtonDown("DebugEvent"))
        {
            eventModule.ShowUI();
        }

        if (Input.GetButtonDown("PlaceConsumer"))
        {
            buildingModule.CreateBuildingAtWorldPos(targetTranslation,Quaternion.Euler(0, 0, 0),Consumer);
        }

        if (Input.GetButtonDown("PlaceProducer"))
        {
            buildingModule.CreateBuildingAtWorldPos(targetTranslation,Quaternion.Euler(0, 0, 0),Producer);
        }

        if (Input.GetButtonDown("PlaceStorage"))
        {
            
        }


    }

}
