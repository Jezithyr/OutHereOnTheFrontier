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
        hudMode = playerHUD.Mode;


        //timer tick
        if(Time.fixedTime%1 == 0)//every second
        {
            GameTimer = GameTimer-1;
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

    private void Pause()
    {
        uiModule.Hide(playerHUD,playerHudId);
        uiModule.Show(pauseMenu,pauseMenuid);
        Game.Pause();
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


}
