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
        PlayerLoopSystem[] unityCoreSubSystems = unityMainLoop.subSystemList;
        PlayerLoopSystem[] unityCoreUpdate = unityCoreSubSystems[4].subSystemList;

        PlayerLoopSystem ScriptModuleUpdate = new PlayerLoopSystem()
        {
            updateDelegate = ScriptModuleUpdateTick,
            type = typeof(PlayerLoop)
        };




        PlayerLoopSystem[] newCoreUpdate = new PlayerLoopSystem[(unityCoreUpdate.Length)];
        newCoreUpdate[(newCoreUpdate.Length-1)] = ScriptModuleUpdate;
        unityCoreSubSystems[4].subSystemList = newCoreUpdate;

        PlayerLoopSystem systemRoot = new PlayerLoopSystem();
        systemRoot.subSystemList = unityCoreSubSystems;
        PlayerLoop.SetPlayerLoop(systemRoot);
    }


    private void ScriptModuleUpdateTick()
    {
        if (!Application.isPlaying) return;
        if (tickTime >= 100000000) tickTime = 0;
        tickTime++;

        foreach (Module module in tickingModules)
        {
            module.Update();
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
