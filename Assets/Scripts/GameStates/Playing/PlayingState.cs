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



    private Vector3 targetTranslation;
    
    private GameManager Game;

    private Camera cam;
    private FreeOrbitCam activeCam;
    public bool BuildMode = false;
    public bool DestroyMode = false;

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
        if(Time.fixedTime%1 == 0)//every second
        {
            GameTimer = GameTimer-1;
        }
        if (GameTimer <= 0)
            {
                GameOver();
            }


        
        //destroy mode is rip :(

        // if (DestroyMode)
        // {
        //     if (Input.GetKeyDown(KeyCode.Escape) && !Game.isPaused)
        //     {
        //         BuildMode = false;
        //         playerHUD.HideBuildingMenu();
        //         DestroyMode = false;
        //     }
        //     if (Input.GetMouseButtonDown(0))
        //     {
        //          RaycastHit  hit;
        //          Ray ray = camModule.ActiveCameraObject.ScreenPointToRay(Input.mousePosition);
                  
        //         if (Physics.Raycast(ray, out hit)) {

        //             Building buildData = buildingModule.GetDataForPrefab(hit.transform.gameObject);
        //             if (buildData.Removable)
        //             {
        //                 buildingModule.RemoveBuilding(hit.transform.gameObject);
        //             }
        //         }
        //     }

        // }




        BuildMode = playerHUD.BuildMode;
        if (BuildMode)
        {
            if (Input.GetKeyDown(KeyCode.Escape) && !Game.isPaused)
            {
                playerHUD.HideBuildingMenu();
            }
            
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



    private void GameOver()
    {
        Game.Pause();
        uiModule.Hide(debugMenu,debugMenuid);
        uiModule.Hide(playerHUD,playerHudId);
        uiModule.Show(gameOverMenu,gameOverMenuid);
    }

    public void SetDestroyMode(bool newMode)
    {
        DestroyMode = newMode;
    }



    private void Pause()
    {
        uiModule.Hide(playerHUD,playerHudId);
        uiModule.Show(pauseMenu,pauseMenuid);
        Game.Pause();
    }

}
