using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "GameFramework/Gamestate/Playing")]
public class PlayingState : GameState
{

    [SerializeField]
    private CameraModule camModule;
    
    [SerializeField] private UIModule uiModule;
    [SerializeField] private PlayerHUD playerHUD;
    [SerializeField] private PauseMenu pauseMenu;
    [SerializeField] private SettingsMenu settingsMenu;
    [SerializeField] private GameState pauseState;
    
    [SerializeField]
    private ScriptedCamera customCamera ;

    
    [SerializeField] private ModuleManager moduleManager;
    [SerializeField] private EventModule eventModule ;
    [SerializeField] private ConstructionModule buildingModule;

    private Vector3 targetTranslation;
    
    private GameManager Game;

    private Camera cam;
    private FreeOrbitCam activeCam;
    public bool BuildMode = false;

    private int playerHudId;
    private int pauseMenuid;
    private int settingsMenuId;
    private int buildingMenuId;
    public int GameTimer;


    private void OnEnable()
    {
        Game = Manager;
    }
    public override void OnActivate(GameState lastState)
    {
        Debug.Log("Entered Playing State");
        ScriptedCamera newCam = camModule.AddScriptedCameraInstance(customCamera);
        activeCam = (FreeOrbitCam)newCam;

        playerHudId = uiModule.CreateInstance(playerHUD);
        pauseMenuid = uiModule.CreateInstance(pauseMenu);
        settingsMenuId = uiModule.CreateInstance(settingsMenu);

        Debug.Log("PlayerHUD UI id: "+ playerHudId);
        Debug.Log("PauseMenu UI id: "+ pauseMenuid);
        Debug.Log("settingsMenuId UI id: "+ settingsMenuId);
        uiModule.Show(playerHUD,playerHudId);
        uiModule.Hide(pauseMenu,pauseMenuid);
        uiModule.Hide(settingsMenu,settingsMenuId);

        GameTimer = 8*60;
    }
    public override void OnDeactivate(GameState newState)
    {

    }

    public void LoadMainScene()
    {
        SceneManager.LoadScene("Main");
    }



    public override void OnUpdate()
    {
        if(Time.frameCount%60 == 0)//every second
        {
            GameTimer = GameTimer-1;
        }

        BuildMode = playerHUD.BuildMode;
        if (BuildMode)
        {
            if (Input.GetMouseButtonDown(0))
            {
                playerHUD.CreateBuildingFromPreview();
            }
        } 
        else 
        {
            if (Input.GetKeyDown(KeyCode.Escape) && !Game.isPaused)
            {
                Pause();
            }
        }
    }

    private void Pause()
    {
        uiModule.Hide(playerHUD,playerHudId);
        uiModule.Show(pauseMenu,pauseMenuid);
        Game.Pause();
    }

}
