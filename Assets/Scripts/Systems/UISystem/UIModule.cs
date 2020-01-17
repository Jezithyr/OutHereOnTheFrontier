using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "GameFramework/SubSystems/UIModule")]
public class UIModule : Module
{
    [SerializeField] 
    public List<ScriptedUI> ActiveInterfaces = new List<ScriptedUI>();
    
    delegate void functionDelegate();
    
    functionDelegate tickDelegate;
    functionDelegate startDelegate;

    public override void Start()
    {
        startDelegate();
    }
    
    public override void Update()
    {
        tickDelegate();
    }

    private void dummyFunc(){}

    private void OnEnable()
    {
        bool hasTickDelegates = false;
        bool hasStartDelegates = false;
        foreach (ScriptedUI userInterface in ActiveInterfaces)
        {
            if (userInterface != null)
            {
            if (userInterface.CanTick)
                {
                    tickDelegate += userInterface.Update;
                    hasTickDelegates = true;
                }  
            }

            if (userInterface.RunOnSceneLoad)
            {
                startDelegate += userInterface.Start;
                hasStartDelegates = true;
            }  
            userInterface.ClearBehaviors();
            
        }
        if (!hasStartDelegates)
        {
            startDelegate = dummyFunc;
        }

        if (!hasTickDelegates)
            {
                tickDelegate = dummyFunc;
            }
            
    }

    public int CreateInstance(ScriptedUI newUI)
    {
        int index = ActiveInterfaces.IndexOf(newUI);
        if (index < 0) return -1;
        return ActiveInterfaces[index].CreateUIInstance();
    }


    public void DestroyInstance(ScriptedUI uiToDestroy, int instanceId)
    {
        if (!ActiveInterfaces.Contains(uiToDestroy)) return;
        int index = ActiveInterfaces.IndexOf(uiToDestroy);
        if (index < 0) return;
        ActiveInterfaces[index].DestroyInstance(instanceId);
    }

    private void Toggle(ScriptedUI ui, int instanceId,bool state)
    {   
        int index = ActiveInterfaces.IndexOf(ui);
        if (index < 0) return;
        ActiveInterfaces[index].ToggleUI(instanceId,state);

    }

    public void Show(ScriptedUI ui, int instanceId) 
    {
        Toggle(ui,instanceId, true);
    }

    public void Hide(ScriptedUI ui, int instanceId) 
    {
        Toggle(ui,instanceId, false);
    }

}
