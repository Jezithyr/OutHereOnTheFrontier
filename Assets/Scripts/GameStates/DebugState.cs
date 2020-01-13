using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DebugState : GameState
{ 
    [SerializeField]
    private CameraModule camModule;
    
    [SerializeField]
    private ScriptedCamera customCamera ;
    public override void OnActivate(GameState lastState)
    {
        Debug.Log("Entered Debug State");
        
        ScriptedCamera newCam = camModule.AddScriptedCameraInstance(customCamera);

        
    }
    public override void OnDeactivate(GameState newState)
    {

    }

    public override void OnUpdate()
    {
        Debug.Log("GameStateTick");
    }

}
