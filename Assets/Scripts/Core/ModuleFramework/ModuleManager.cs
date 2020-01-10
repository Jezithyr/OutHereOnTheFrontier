using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "GameFramework/Core/ModuleManager")]
public class ModuleManager : ScriptableObject
{
    [SerializeField] 
    private GameManager gameManager;

    delegate void TickModuleUpdateDelegate();


    [SerializeField]
    private List<Module> ActiveModules;
    private Dictionary<System.Type,Module> moduleList = new Dictionary<System.Type,Module>();
    public Dictionary<System.Type,Module> List {get => moduleList;}

    private List<TickModuleUpdateDelegate> tickingModules = new List<TickModuleUpdateDelegate>();

    public void ModuleUpdateTick()
    {
        if (!Application.isPlaying) return;
        foreach (TickModuleUpdateDelegate moduleTickFunc in tickingModules)
        {
            moduleTickFunc();
        }
    }


    public void Initialize()
    {
        Debug.Log("-= Loading Game framework modules =-\n");
        LoadModules();
        Debug.Log("-Module initalization Complete-\n");
    }


    public void LoadModules()
    {
        moduleList.Clear();

        Debug.Log("=Building Module List=\n");
        string debugModuleList = "";
        
        foreach (var module in ActiveModules)
        {
            moduleList.Add(module.GetType(),module);
            debugModuleList = debugModuleList + "" + module+",";
        }
        Debug.Log(debugModuleList+"\n");


        Debug.Log("-Module List built successfully-\n");

        Debug.Log("--==Initializing Modules==-\n");
        foreach (Module module in ActiveModules)
        {
            Debug.Log("Loading: " + module.GetType()+"\n");
            module.Initialize();
            if (module.RunUpdate)
            {
                tickingModules.Add(module.Update);
                Debug.Log("" + module + " Added to update thread\n");
            }
            Debug.Log(module.GetType() +" Load Complete\n");
        }
        
    }


 public T GetModule<T>() where T : Module
    {
        foreach (var item in moduleList)
        {
            if (item.Key == typeof(T)) return (T)item.Value;
        }
        return default(T);
    }



}
