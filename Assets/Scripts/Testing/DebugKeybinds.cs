using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugKeybinds : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private ModuleManager moduleManager;
    [SerializeField] private EventModule eventModule ;
    [SerializeField] private ConstructionModule buildingModule;
    [SerializeField] private Building Producer;
    [SerializeField] private Building Consumer;
    private Vector3 targetTranslation;

    private Camera cam;
    private FreeOrbitCam activeCam;
    private void Start()
    {
        cam = moduleManager.GetModule<CameraModule>().ActiveCamera.CreatedCamera;
        activeCam = (FreeOrbitCam)(moduleManager.GetModule<CameraModule>().ActiveCamera);

    }

    private void Update()
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
