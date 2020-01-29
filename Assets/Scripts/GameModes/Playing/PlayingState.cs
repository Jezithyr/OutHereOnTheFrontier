using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "GameFramework/Gamestate/Playing")]
public class PlayingState : GameState
{

    [SerializeField]
    private CameraModule camModule;
    
    [SerializeField] private UIModule uiModule;
    [SerializeField] private PlayerHUD playerHUD;
    [SerializeField] private ScriptedUI buildMenu;
    
    [SerializeField]
    private ScriptedCamera customCamera ;

    
    [SerializeField] private ModuleManager moduleManager;
    [SerializeField] private EventModule eventModule ;
    [SerializeField] private ConstructionModule buildingModule;
    [SerializeField] private Building Producer;
    [SerializeField] private Building Consumer;

    [SerializeField] private string BuildingHotkey = "OpenBuildMenu";

    private Vector3 targetTranslation;

    private Camera cam;
    private FreeOrbitCam activeCam;
    public bool BuildMode = false;

    private int playerHudId;
    private int buildingMenuId;

    public override void OnActivate(GameState lastState)
    {
         Debug.Log("Entered Playing State");
        
        ScriptedCamera newCam = camModule.AddScriptedCameraInstance(customCamera);
        activeCam = (FreeOrbitCam)newCam;

        playerHudId = uiModule.CreateInstance(playerHUD);
        Debug.Log("PlayerHUD UI id: "+ playerHudId);
        uiModule.Show(playerHUD,playerHudId);
    }
    public override void OnDeactivate(GameState newState)
    {


    }

    public override void OnUpdate()
    {

        BuildMode = playerHUD.BuildMode;
        if (BuildMode)
        {
            if (Input.GetMouseButtonDown(0))
            {
                playerHUD.CreateBuildingFromPreview();
            }



            
        }


    }
}
