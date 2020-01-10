using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "GameFramework/SubSystems/CameraModule")]
public class CameraModule : Module
{
    private ScriptedCamera activeCamera;
    public ScriptedCamera ActiveCamera{get => activeCamera;}

    public override void Initialize()
    {
        
    }

    private List<ScriptedCamera> ScriptedCameras;

    public override void Update()
    {
        if (activeCamera)
        {
            activeCamera.CameraUpdate();
        }
    }

    public ScriptedCamera AddScriptedCameraInstance(ScriptedCamera newCamera)
    {
        activeCamera = AddScriptedCamera(ScriptableObject.Instantiate(newCamera));
        return activeCamera;
    }


    public ScriptedCamera AddScriptedCamera(ScriptedCamera newCamera)
    {
        Debug.Log("NewCamera = " + newCamera);
        newCamera.Initalize();
        newCamera.Active = true;
        ScriptedCameras.Add(newCamera);
        return ScriptedCameras[ScriptedCameras.Count-1];
    }

    public void ActivateCamera(ScriptedCamera newCamera)
    {
        if (newCamera == activeCamera) return;

        foreach (var scriptCam in ScriptedCameras)
        {
            if (scriptCam == newCamera)
            {
                activeCamera = newCamera;
            }
        }
    }

}
