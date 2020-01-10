using System.Collections;
using System.Collections.Generic;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.Experimental.LowLevel;
using UnityEngine;


public class GameManager : ScriptableObject
{   
    [SerializeField] 
    private ModuleManager moduleManager;
    
    [SerializeField]
    private List<GameState> gameStates = new  List<GameState>();

    [SerializeField]
    private List<GameStateCondition> stateCondition = new  List<GameStateCondition>();

    GameState ActiveState;
    void GameStateUpdate()
    {
        if (ActiveState && ActiveState.CanTick) 
        {
            ActiveState.OnUpdate();
        }

        for (int i = 0; i < gameStates.Count; i++)
        {
            if (stateCondition[i].ConditionCheck(ActiveState))
            {
                gameStates[i].OnActivate(ActiveState);
                ActiveState.OnDeactivate(gameStates[i]);
                ActiveState = gameStates[i];
                break; //break out of the for loop if there is a state change
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
            updateDelegate = moduleManager.ModuleUpdateTick,
            type = typeof(PlayerLoop)
        };

        PlayerLoopSystem gameStateUpdate = new PlayerLoopSystem()
        {
            updateDelegate = GameStateUpdate,
            type = typeof(PlayerLoop)
        };

        PlayerLoopSystem[] newCoreUpdate = new PlayerLoopSystem[(unityCoreUpdate.Length)];
        newCoreUpdate[(newCoreUpdate.Length-1)] = ScriptModuleUpdate;
        unityCoreSubSystems[4].subSystemList = newCoreUpdate;

        PlayerLoopSystem systemRoot = new PlayerLoopSystem();
        systemRoot.subSystemList = unityCoreSubSystems;
        PlayerLoop.SetPlayerLoop(systemRoot);
    }

    private void OnEnable()
    {
        moduleManager.LoadModules();

        Debug.Log("----------------------------------\n");
        Debug.Log("Loading Gamestates\n");
        
        bool hasValidStates = stateCondition.Count >= 0;
        
        if (stateCondition.Count != gameStates.Count) 
        {
            Debug.LogError("Error: mismatching gamestates and conditions");
            hasValidStates = false;
        }
        
        int index = 0;
        while (hasValidStates && index < gameStates.Count)
        {

            if (stateCondition[index] == null || gameStates[index] == null)
            {
                Debug.LogError("Error: A gamestate or condition is undefined!");
                hasValidStates = false;
            }
            index++;
        }
        Debug.Log("Finsihed Loading Gamestates\n");

        Debug.Log("===============================\n");
        Debug.Log("======Initializion Complete======\n");
        Debug.Log("===============================\n");
    }              






    public T GetModule<T>() where T : Module
    {
        return moduleManager.GetModule<T>();
    }
}
