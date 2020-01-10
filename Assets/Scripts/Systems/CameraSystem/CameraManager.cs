using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraManager : Module
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
        return AddScriptedCamera(ScriptableObject.Instantiate(newCamera));
    }


    public ScriptedCamera AddScriptedCamera(ScriptedCamera newCamera)
    {
        ScriptedCamera tempCam = newCamera;
        tempCam.Initalize();
        tempCam.Active = true;
        ScriptedCameras.Add(tempCam);
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
