using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ModuleManager : ScriptableObject
{
    [SerializeField] 
    private GameManager gameManager;

    [SerializeField]
    private List<Module> ActiveModules;
    private Dictionary<System.Type,Module> moduleList = new Dictionary<System.Type,Module>();
    public Dictionary<System.Type,Module> List {get => moduleList;}

    private List<Module> tickingModules = new List<Module> ();

    public void ModuleUpdateTick()
    {
        if (!Application.isPlaying) return;

        foreach (Module module in tickingModules)
        {
            module.Update();
        }
    }


    public void Initialize()
    {
        LoadModules();
        Debug.Log("Module Load Complete\n");
    }


    public void LoadModules()
    {
        moduleList.Clear();

        Debug.Log("=Building Module List=\n");
        foreach (var module in ActiveModules)
        {
            moduleList.Add(module.GetType(),module);
        }
        Debug.Log("-Module List built successfully-\n");

        Debug.Log("--==Initializing Modules==-\n");
        foreach (Module module in ActiveModules)
        {
            Debug.Log("Loading: " + module.GetType()+"\n");
            module.Initialize();
            if (module.RunUpdate)
            {
                tickingModules.Add(module);
                Debug.Log("" + module.GetType() + " Added to update thread\n");
            }
            Debug.Log(module.GetType() +" Load Complete\n");
        }
        Debug.Log("-Module initalization Complete-\n");
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
