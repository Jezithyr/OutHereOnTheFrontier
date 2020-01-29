using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "GameFramework/SubSystems/CameraModule")]
public class CameraModule : Module
{
    private ScriptedCamera activeCamera;
    public ScriptedCamera ActiveCamera{get => activeCamera;}

    private Camera activeCameraObj;
    public Camera ActiveCameraObject{get => activeCameraObj;}

    public override void Initialize()
    {
        
    }

    private List<ScriptedCamera> ScriptedCameras = new List<ScriptedCamera>();

    public override void Update()
    {
        if (activeCamera)
        {
            activeCamera.CameraUpdate();
        }
    }

    public ScriptedCamera AddScriptedCameraInstance(ScriptedCamera newCamera)
    {
        Debug.LogWarning("Creating: "+ newCamera);
        activeCamera = AddScriptedCamera(ScriptableObject.Instantiate(newCamera));
        activeCamera.Active = true;
        
        activeCameraObj = activeCamera.CreatedCamera;
        return activeCamera;
    }


    public ScriptedCamera AddScriptedCamera(ScriptedCamera newCamera)
    {
        Debug.Log("NewCamera = " + newCamera);
        newCamera.Initalize();
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
                newCamera.Active = true;
                activeCameraObj = newCamera.CreatedCamera;
            }
        }

    }

    public void RemoveScriptedCamera(ScriptedCamera cam)
    {
        ScriptedCameras.Remove(cam);
    }

}
