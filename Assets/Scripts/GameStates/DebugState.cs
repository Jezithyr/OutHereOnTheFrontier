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

    private int playerHudId;

    public override void OnActivate(GameState lastState)
    {
        Debug.Log("Entered Debug State");
        
        ScriptedCamera newCam = camModule.AddScriptedCameraInstance(customCamera);
        playerHudId = uiModule.CreateInstance(playerHUD);
        Debug.Log("PlayerHUD UI id: "+ playerHudId);
        uiModule.Show(playerHUD,playerHudId);
    }
    public override void OnDeactivate(GameState newState)
    {

    }

    public override void OnUpdate()
    {
        Debug.Log("GameStateTick");
    }

}
