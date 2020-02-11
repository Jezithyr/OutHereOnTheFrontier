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
    //[SerializeField] private EventPopup eventMenu;
    [SerializeField] private DebugMenu debugMenu ;
    [SerializeField] private GameOver gameOverMenu ;
    [SerializeField] private GameState pauseState;
    
    [SerializeField]
    private ScriptedCamera customCamera ;

    
    [SerializeField] private ModuleManager moduleManager;
    [SerializeField] private EventModule eventModule ;
    [SerializeField] private ConstructionModule buildingModule;



    [SerializeField] private List<Event> eventList = new List<Event>();//only use 4 or everything breaks
    [SerializeField] private LayerMask buildingLayer;

    [SerializeField] private int gameTimer =  8*60;


    private Vector3 targetTranslation;
    
    private GameManager Game;

    private Camera cam;
    private FreeOrbitCam activeCam;


    private PlayerHudModes hudMode;
    private int playerHudId;
    private int pauseMenuid;
    private int settingsMenuId;
    private int buildingMenuId;
    private int gameOverMenuid;
    //private int eventMenuId;
    private int debugMenuid;

    //todo make these not public
    public int GameTimer;
    public int ElapsedTime = 0;


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

        gameOverMenuid = uiModule.CreateInstance(gameOverMenu);

        //eventMenuId = uiModule.CreateInstance(eventMenu);
        debugMenuid = uiModule.CreateInstance(debugMenu);

        Debug.Log("PlayerHUD UI id: "+ playerHudId);
        Debug.Log("PauseMenu UI id: "+ pauseMenuid);
        Debug.Log("settingsMenuId UI id: "+ settingsMenuId);
        uiModule.Show(playerHUD,playerHudId);
        
        //uiModule.Hide(eventMenu,debugMenuid);

        
        eventModule.InitializePrefab();

        uiModule.Hide(gameOverMenu,gameOverMenuid);
        uiModule.Hide(pauseMenu,pauseMenuid);
        //uiModule.Hide(eventMenu,eventMenuId);
        uiModule.Hide(settingsMenu,settingsMenuId);

        ElapsedTime = 0;
        GameTimer = gameTimer;
        Game.UnPause();
    }
    public override void OnDeactivate(GameState newState)
    {
        Reset();
            
        SceneManager.LoadScene(sceneName:"MainMenu");

        //SceneManager.SetActiveScene(SceneManager.GetSceneByName("MainMenu"));
    }

    public void LoadMainScene()
    {
        SceneManager.LoadScene("Main");
    }

    public override void OnUpdate()
    {
        if (Time.timeScale == 0) return; //stop update if timescale is 0
        hudMode = playerHUD.Mode;


        //timer tick
        if(Time.timeScale > 0 && Time.fixedTime%1 == 0)//every second
        {
            GameTimer = GameTimer-1;
            ElapsedTime += 1;
        }
        if (GameTimer <= 0)
            {
                GameOver();
            }

        switch (hudMode)
        {
            case PlayerHudModes.Building: 
            {
                 if (Input.GetKeyDown(KeyCode.Escape) && !Game.isPaused)
                {
                    playerHUD.HideBuildingMenu();
                    playerHUD.SetHudMode(0);
                }
            
                if (Input.GetMouseButtonDown(0))
                {
                    playerHUD.CreateBuildingFromPreview();
                }

                break;
            };


            case PlayerHudModes.Demolishing: 
            {
                 if (Input.GetKeyDown(KeyCode.Escape) && !Game.isPaused)
                {
                    playerHUD.HideDemolishExit();
                    playerHUD.SetHudMode(0);
                }

                if (Input.GetMouseButtonDown(0))
                {
                    TryDemolishUnderCursor();
                }

                break;
            };

            default:
            {
                if (Input.GetKeyDown(KeyCode.Escape) && !Game.isPaused)
                {
                    Pause();
                }
                break;
            }
        }
    }

    private void GameOver()
    {
        Game.Pause();
        uiModule.Hide(debugMenu,debugMenuid);
        uiModule.Hide(playerHUD,playerHudId);
        uiModule.Show(gameOverMenu,gameOverMenuid);
    }

    public void ReturnToMenu()
    {
        Game.SwitchSystemGameState(0);
    }

    public void PauseGame()
    {
        Game.Pause();
    }

    public void UnPauseGame()
    {
        Game.UnPause();
    }
    private void Pause()
    {
        uiModule.Hide(playerHUD,playerHudId);
        uiModule.Show(pauseMenu,pauseMenuid);
        PauseGame();
    }

    private void TryDemolishUnderCursor()
    {
        if (activeCam.CreatedCamera == null) return;
        RaycastHit hit = uiModule.CursorRaycast(activeCam.CreatedCamera,buildingLayer);
        if (!hit.collider) return;

        Building hitBuilding = buildingModule.GetDataForPrefab(hit.collider.gameObject);
        if (!hitBuilding) return;

        if (hitBuilding.Removable)
        {
            hitBuilding.Deconstruct(hit.collider.gameObject);
        }
    }

    public override void Reset()
    {
        uiModule.DestroyInstance(playerHUD,playerHudId);
        uiModule.DestroyInstance(pauseMenu,pauseMenuid);
        uiModule.DestroyInstance(settingsMenu,settingsMenuId);
        uiModule.DestroyInstance(gameOverMenu,gameOverMenuid);

        Game.Pause();
        camModule.Reset();
        buildingModule.Reset();
        Game.GetModule<ResourceModule>().Reset();//reset resource module
        uiModule.Reset(); //not needed?
    }
}
