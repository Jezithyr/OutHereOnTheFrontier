using System.Collections;
using System.Collections.Generic;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.Experimental.LowLevel;
using UnityEngine;


public class GameManager : ScriptableObject
{   
    [SerializeField] private ModuleManager moduleManager;
    

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

        Debug.Log("===============================\n");
        Debug.Log("======Initializion Complete======\n");
        Debug.Log("===============================\n");
    }              

    public T GetModule<T>() where T : Module
    {
        return moduleManager.GetModule<T>();
    }
}
