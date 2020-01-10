using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DebugState : GameState
{ 
    
    [SerializeField] private ScriptedCamera customCamera ;
    public override void OnActivate(GameState lastState)
    {
        Debug.Log("Entered Debug State");
        
        //CameraModule camModule = Manager.GetModule<CameraModule>();
        // ScriptedCamera newCam = camModule.AddScriptedCameraInstance(customCamera);
        // camModule.ActivateCamera(newCam);
        

    }
    public override void OnDeactivate(GameState newState)
    {

    }

    public override void OnUpdate()
    {
        Debug.Log("GameStateTick");
    }

}
