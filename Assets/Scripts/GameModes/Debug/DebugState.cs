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

    [SerializeField] private ModuleManager moduleManager;
    [SerializeField] private EventModule eventModule ;
    [SerializeField] private ConstructionModule buildingModule;
    [SerializeField] private Building Producer;
    [SerializeField] private Building Consumer;
    private Vector3 targetTranslation;

    private Camera cam;
    private FreeOrbitCam activeCam;



    private Vector2 testMousePos;
    private GameObject testOBJ;
    private Vector3 debugCoords;
    int layerMask;

    private int playerHudId;

    public override void OnActivate(GameState lastState)
    {
        Debug.Log("Entered Debug State");
        
        ScriptedCamera newCam = camModule.AddScriptedCameraInstance(customCamera);
        activeCam = (FreeOrbitCam)newCam;


        playerHudId = uiModule.CreateInstance(playerHUD);
        Debug.Log("PlayerHUD UI id: "+ playerHudId);
        uiModule.Show(playerHUD,playerHudId);
        layerMask = LayerMask.GetMask("BuildingPlacement");

        testOBJ = GameObject.CreatePrimitive(PrimitiveType.Cube);
    }
    public override void OnDeactivate(GameState newState)
    {

    }

    public override void OnUpdate()
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
        
        debugCoords =  uiModule.CursorToWorld(activeCam.CreatedCamera,layerMask);

        //Math.Round(value / 5.0) * 5;

        debugCoords = new Vector3(Mathf.Round(debugCoords.x/1f)*1f+0.5f,debugCoords.y,Mathf.Round(debugCoords.z/1f)*1f+0.5f);
        testOBJ.transform.position = debugCoords;
        Debug.Log(debugCoords);

    }

}
