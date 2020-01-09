using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraManager : ScriptableObject
{
    [SerializeField] private GameManager gameManager;

    [SerializeField] private ScriptedCamera activeCamera;
    public ScriptedCamera ActiveCamera{get => activeCamera;}

    private List<ScriptedCamera> ScriptedCameras;

    public ScriptedCamera AddScriptedCamera(ScriptedCamera newCamera)
    {
        Debug.Log("Creating new Camera");
        ScriptedCamera tempCam = ScriptableObject.Instantiate(newCamera);
        tempCam.Initalize();
        ScriptedCameras.Add(tempCam);
        return ScriptedCameras[ScriptedCameras.Count-1];
    }

    public void ActivateCamera(ScriptedCamera newCamera)
    {
        Debug.Log("Activating Camera");
        if (newCamera == activeCamera) return;

        foreach (var scriptCam in ScriptedCameras)
        {
            if (scriptCam == newCamera)
            {
                activeCamera = newCamera;
            }
        }
    }

    private void Update()
    {
        if (ActiveCamera)
        {
            ActiveCamera.Active = true;
        }
    }

}
