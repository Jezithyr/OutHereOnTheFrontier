using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTest : MonoBehaviour
{
    [SerializeField] protected CameraModule cameraManager;
    [SerializeField] private ScriptedCamera customCamera;
    // Start is called before the first frame update
    void Start()
    {
        //ScriptedCamera newCam = cameraManager.AddScriptedCamera(customCamera);
       // cameraManager.ActivateCamera(newCam);
    }

    
}
