﻿using System.Collections;
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
    [SerializeField] private EventPopup eventMenu;
    [SerializeField] private DebugMenu debugMenu ;
    [SerializeField] private GameOver gameOverMenu ;
    [SerializeField] private GameState pauseState;
    
    [SerializeField]
    private ScriptedCamera customCamera ;

    
    [SerializeField] private ModuleManager moduleManager;
    [SerializeField] private EventModule eventModule ;
    [SerializeField] private ConstructionModule buildingModule;
    [SerializeField] private ResourceModule resourceModule;
    [SerializeField] private Resource populationResource;


    [SerializeField] private List<Event> eventList = new List<Event>();//only use 4 or everything breaks
    [SerializeField] private LayerMask buildingLayer;

    [SerializeField] private int gameTimer =  8*60;

    [SerializeField] private AudioClip demolishSound;
    [SerializeField] private AudioClip ambience;
    public AudioClip Ambiance{get => ambience;}


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
    private byte gameOverType = 0;
    private AudioSource audioSource2D;
    public AudioSource AudioSource2D{get => audioSource2D;}

    private int eventMenuId;
    private int debugMenuid;

    //todo make these not public
    public int GameTimer;
    public int ElapsedTime = 0;
    protected bool initalized = false;

    private void OnEnable()
    {
        Game = Manager;
    }


    public override void OnActivate(GameState lastState)
    {
        
        Debug.Log("Entered Playing State");
        
        ScriptedCamera newCam = camModule.AddScriptedCameraInstance(customCamera);
        activeCam = (FreeOrbitCam)newCam;
        audioSource2D = newCam.CreatedCamera.transform.parent.gameObject.GetComponentInChildren<AudioSource>();
        Debug.Log(audioSource2D);

        playerHudId = uiModule.CreateInstance(playerHUD);
        pauseMenuid = uiModule.CreateInstance(pauseMenu);
        settingsMenuId = uiModule.CreateInstance(settingsMenu);

        gameOverMenuid = uiModule.CreateInstance(gameOverMenu);

        
        eventMenuId = uiModule.CreateInstance(eventMenu);

        debugMenuid = uiModule.CreateInstance(debugMenu);

        Debug.Log("PlayerHUD UI id: "+ playerHudId);
        Debug.Log("PauseMenu UI id: "+ pauseMenuid);
        Debug.Log("settingsMenuId UI id: "+ settingsMenuId);
        uiModule.Show(playerHUD,playerHudId);
        
        uiModule.Hide(eventMenu,eventMenuId);

        if (Debug.isDebugBuild)
        {
            uiModule.Hide(debugMenu,debugMenuid);
        }
        

        eventModule.Start();

        uiModule.Hide(gameOverMenu,gameOverMenuid);
        uiModule.Hide(pauseMenu,pauseMenuid);
        uiModule.Hide(eventMenu,eventMenuId);
        uiModule.Hide(settingsMenu,settingsMenuId);

        ElapsedTime = 0;
        GameTimer = gameTimer;
        Game.UnPause();
        audioSource2D.clip = ambience;
        audioSource2D.loop = true;
        audioSource2D.Play();
    }
    public override void OnDeactivate(GameState newState)
    {
        Reset();
            
        SceneManager.LoadScene(sceneName:"MainMenu");
        uiModule.Hide(gameOverMenu,gameOverMenuid);
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
        if(Time.frameCount%60 <= 0)//every second
        {
            GameTimer = GameTimer-1;
            ElapsedTime += 1;
            
        }

        if (resourceModule.GetResourceStorage(populationResource) < 1)
        {
            gameOverType = 1;
            GameOver();
        }

        if (GameTimer <= 0)
        {
            gameOverType = 0;
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
                    audioSource2D.PlayOneShot(demolishSound);
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

    public void Win()
    {
        uiModule.Hide(debugMenu,debugMenuid);
        uiModule.Hide(playerHUD,playerHudId);
        uiModule.Hide(eventMenu,eventMenuId);
        gameOverMenu.ChangeGameOverReason("You win!");
        gameOverMenu.HideLoseText();
        uiModule.Show(gameOverMenu,gameOverMenuid);
        Game.Pause();
    }

    private void GameOver()
    {
        Game.Pause();
        uiModule.Hide(debugMenu,debugMenuid);
        uiModule.Hide(playerHUD,playerHudId);
        uiModule.Hide(eventMenu,eventMenuId);

        switch(gameOverType)
        {
            case 0: 
            {
                gameOverMenu.ChangeGameOverReason("Outta time!");
                break;
            }
            case 1:
            {
                gameOverMenu.ChangeGameOverReason("Everyone Died");
                break;
            }
        }



        uiModule.Show(gameOverMenu,gameOverMenuid);
    }

    public void ReturnToMenu()
    {
        Reset();
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
        uiModule.DestroyInstance(eventMenu,eventMenuId);
        uiModule.DestroyInstance(settingsMenu,settingsMenuId);
        uiModule.DestroyInstance(gameOverMenu,gameOverMenuid);
        eventMenu.cleanup();

        Game.Pause();
        camModule.Reset();
        eventModule.Reset();
        buildingModule.Reset();
        Game.GetModule<ResourceModule>().Reset();//reset resource module
        uiModule.Reset(); //not needed?
    }
}
