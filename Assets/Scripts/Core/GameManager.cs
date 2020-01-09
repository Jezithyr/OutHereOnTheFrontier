using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : ScriptableObject
{
    [SerializeField]
    private List<Module> ActiveModules;
    private Dictionary<System.Type,Module> ModuleList = new Dictionary<System.Type,Module>();
    
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
            Debug.Log("Complete\n");
        }
        Debug.Log("-Module initalization Complete-");
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
