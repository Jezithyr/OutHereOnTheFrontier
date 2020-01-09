using System.Collections;
using System.Collections.Generic;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.Experimental.LowLevel;
using UnityEngine;


public class GameManager : ScriptableObject
{

    [SerializeField]
    private List<Module> ActiveModules;
    private Dictionary<System.Type,Module> ModuleList = new Dictionary<System.Type,Module>();

    delegate void UpdateDelegate();
    private List<Module> tickingModules = new List<Module> ();
    long tickTime = 0;

    private void MainLoopInit()
    {
        PlayerLoopSystem unityMainLoop = PlayerLoop.GetDefaultPlayerLoop();

        PlayerLoopSystem ScriptModuleUpdate = new PlayerLoopSystem()
        {
            updateDelegate = ScriptModuleUpdateTick,
            type = typeof(PlayerLoop)
        };




        PlayerLoopSystem[] tempArray = new PlayerLoopSystem[(unityMainLoop.subSystemList.Length)];
        tempArray = unityMainLoop.subSystemList;
        tempArray[(unityMainLoop.subSystemList.Length-1)] = ScriptModuleUpdate;


        PlayerLoopSystem systemRoot = new PlayerLoopSystem();
        systemRoot.subSystemList = tempArray;
        PlayerLoop.SetPlayerLoop(systemRoot);
    }


    private void ScriptModuleUpdateTick()
    {
        if (!Application.isPlaying) return;
        if (tickTime >= 100000000) tickTime = 0;
        tickTime++;

        foreach (Module module in tickingModules)
        {
            if (tickTime%module.TickTime == 0)
            {
                module.Update();
            }
        }
    }

    private void OnEnable()
    {
        ModuleList.Clear();

        Debug.Log("=Building Module List=\n");
        foreach (var module in ActiveModules)
        {
            ModuleList.Add(module.GetType(),module);
        }
        Debug.Log("-Module List built successfully-");

        Debug.Log("--==Initializing Modules==-");
        foreach (Module module in ActiveModules)
        {
            Debug.Log("Loading: " + module.GetType());
            module.Initialize();
            if (module.RunUpdate)
            {
                tickingModules.Add(module);
                if (module.TickTime <= 0)
                {
                    module.TickTime = 1;
                }
                Debug.Log("" + module.GetType() + " Added to update thread");
            }
            Debug.Log("Complete\n");
        }
        Debug.Log("-Module initalization Complete-");

        Debug.Log("Injecting ScriptModule Updates into Update Loop");
        MainLoopInit();
    }

    public T GetModule<T>() where T : Module
    {
        foreach (var item in ModuleList)
        {
            if (item.Key == typeof(T)) return (T)item.Value;
        }
        return default(T);
    }
}
