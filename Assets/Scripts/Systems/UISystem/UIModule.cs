using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[CreateAssetMenu(menuName = "GameFramework/SubSystems/UIModule")]
public class UIModule : Module
{
    [SerializeField] 
    public List<ScriptedUI> ActiveInterfaces = new List<ScriptedUI>();
    
    public GameObject LinkedEventSystem;

    delegate void functionDelegate();
    
    functionDelegate tickDelegate;
    functionDelegate startDelegate;

    public override void Start()
    {
        CreateUnityEventSystem();
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

    private void CreateUnityEventSystem()
    {
        LinkedEventSystem = new GameObject("EventSystem", typeof(EventSystem), typeof(StandaloneInputModule));
    }

    public Vector3 CursorToWorld(Camera thisCamera,int layermask)
    {
        Ray ray = thisCamera.ScreenPointToRay(Input.mousePosition);
        
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layermask))
        {
            return hit.point;
        }
        return new Vector3(0,0,0);
    }
}
