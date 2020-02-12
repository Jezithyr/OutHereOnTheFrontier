using System.Collections;
using System.Collections.Generic;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.Experimental.LowLevel;
using UnityEngine.SceneManagement;
using UnityEngine.Scripting;
using UnityEngine;

[CreateAssetMenu(menuName = "GameFramework/Core/GameManager")]
public class GameManager : ScriptableObject
{   
    [SerializeField] 
    private ModuleManager LinkedModuleManager;
    public ModuleManager moduleManager{get =>LinkedModuleManager;}

    [Header("Game State System")]
    [SerializeField]
    private List<GameState> gameStates = new  List<GameState>();

    [SerializeField]
    private List<GameStateCondition> stateCondition;

    [SerializeField]
    private List<GameState> systemGameStates = new  List<GameState>();

    [SerializeField] public GameState InitialGameState;

    private GameState ActiveState;
    private Scene ActiveScene;

    private float lastTimescale = 1;

    private bool Paused = false;
    public bool isPaused{get => Paused;}


    delegate void OnSceneLoadedDelegate();

    public void Pause()
    {
        lastTimescale = Time.timeScale;
        Time.timeScale = 0;
        moduleManager.StopTicking = true;
        Paused = true;
    }

    public void UnPause()
    {
        Time.timeScale = 1;
        moduleManager.StopTicking = false;
        Paused = false;
    }

    
    void GameStateUpdate()
    {
        if (!Application.isPlaying) return;

        if (ActiveState && ActiveState.CanTick) 
        {
            ActiveState.OnUpdate();
        }

        for (int i = 0; i < gameStates.Count; i++)
        {
            if (stateCondition[i].ConditionCheck(ActiveState))
            {
                if (stateCondition[i] && (!ActiveState.Equals(gameStates[i]))){
                    gameStates[i].OnActivate(ActiveState);
                    ActiveState.OnDeactivate(gameStates[i]);
                    ActiveState = gameStates[i];
                    break; //break out of the for loop if there is a state change
                }
            }
        }
    }
   

    private void MainLoopInit()
    {
        PlayerLoopSystem unityMainLoop = PlayerLoop.GetDefaultPlayerLoop();
        PlayerLoopSystem[] unityCoreSubSystems = unityMainLoop.subSystemList;
        PlayerLoopSystem[] unityCoreUpdate = unityCoreSubSystems[4].subSystemList;
        PlayerLoopSystem ScriptModuleUpdate = new PlayerLoopSystem()
        {
            updateDelegate = LinkedModuleManager.ModuleUpdateTick,
            type = typeof(PlayerLoop)
        };

        PlayerLoopSystem gameStateUpdate = new PlayerLoopSystem()
        {
            updateDelegate = GameStateUpdate,
            type = typeof(PlayerLoop)
        };

        PlayerLoopSystem[] newCoreUpdate = new PlayerLoopSystem[(unityCoreUpdate.Length+1)];
        newCoreUpdate[0] = gameStateUpdate;
        newCoreUpdate[1] = ScriptModuleUpdate;
        newCoreUpdate[2] = unityCoreUpdate[0];
        newCoreUpdate[3] = unityCoreUpdate[1];
        newCoreUpdate[4] = unityCoreUpdate[2];

        unityCoreSubSystems[4].subSystemList = newCoreUpdate;

        PlayerLoopSystem systemRoot = new PlayerLoopSystem();
        systemRoot.subSystemList = unityCoreSubSystems;
        PlayerLoop.SetPlayerLoop(systemRoot);
    }

    private void OnEnable()
    {
        if (!Application.isEditor) return;
         Initalize();
         Debug.Log("Running GameManager in Editor");
    }

    private void Awake()
    {
        if (Application.isEditor) return;
         Initalize();
         Debug.Log("Running GameManager in Build");
    }

    private void Initalize()
    {

        SceneManager.sceneLoaded +=  OnSceneLoad;

        LinkedModuleManager.Initialize();

        Debug.Log("----------------------------------\n");
        Debug.Log("Loading Gamestates\n");
        
        bool hasValidStates = stateCondition.Count >= 0;
        
        if (stateCondition.Count != gameStates.Count) 
        {
            Debug.LogError("Error: mismatching gamestates and conditions\n");
            hasValidStates = false;
        }
        
        int index = 0;
        while (hasValidStates && index < gameStates.Count)
        {
            gameStates[index].Manager = this;
            gameStates[index].Initalize();
            if (stateCondition[index] == null || gameStates[index] == null)
            {
                Debug.LogError("Error: A gamestate or condition is undefined!\n");
                hasValidStates = false;
            }
            index++;
        }
        Debug.Log("Finished Loading Gamestates\n");
        Debug.Log("Adding Delegates to UnityUpdate\n");
        MainLoopInit();
        Debug.Log("Done\n");

        Debug.Log("===============================\n");
        Debug.Log("======Initializion Complete======\n");
        Debug.Log("===============================\n");
    }


    public void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("SceneLoaded");
        
        moduleManager.ModuleStartOnLoad();
        
        if (scene.name == "MainMenu")
        {
            Start();
        }
        else
        {
            ActiveState.OnActivate(null);
        }
    }


    public void SwitchSystemGameState(int index)
    {
        GameState lastState = ActiveState;
        ActiveState.OnDeactivate(ActiveState);
        ActiveState = systemGameStates[index];
        //ActiveState.OnActivate(lastState);
    }

    public void Start()
    {
        ActiveState = InitialGameState;
        ActiveState.OnActivate(null);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public T GetModule<T>() where T : Module
    {
        return LinkedModuleManager.GetModule<T>();
    }
}
