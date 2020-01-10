using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName= "temp")]
public class DebugState : GameState
{ 
    
    [SerializeField] private ScriptedCamera customCamera ;
    public override void OnActivate(GameState lastState)
    {
        Debug.Log("Entered Debug State");
        
        CameraManager camModule = Manager.GetModule<CameraManager>();
        Debug.Log("Cam Module = " + camModule);
        ScriptedCamera newCam = camModule.AddScriptedCamera(customCamera);
        camModule.ActivateCamera(newCam);

    }
    public override void OnDeactivate(GameState newState)
    {

    }

    public override void OnUpdate()
    {
        
    }

}
