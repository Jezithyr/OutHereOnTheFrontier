using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[CreateAssetMenu(menuName = "GameFramework/SubSystems/UIModule")]
public class UIModule : Module
{
    [SerializeField] 
    public List<ScriptedUI> ActiveInterfaces = new List<ScriptedUI>(); //list of interfaces that can be referenced


    private GameObject linkedEventSystem;

    public GameObject LinkedEventSystem{get => linkedEventSystem;}

    [SerializeField] private AudioClip clickSound;
    [SerializeField] private AudioClip failSound;
    //[SerializeField] private AudioClip successSound;//not implemented
    [SerializeField] private AudioClip alertSound;

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


    private void AddUpdateDelegate(functionDelegate newdelegate)
    {
        tickDelegate += newdelegate;
    }

    private void DeleteUpdateDelegate(functionDelegate newdelegate)
    {
        tickDelegate -= newdelegate;
    }


    private void AddStartDelegate(functionDelegate newdelegate)
    {
        startDelegate += newdelegate;
    }

    private void DeleteStartDelegate(functionDelegate newdelegate)
    {
        startDelegate -= newdelegate;
    }


    
    public void playClickSound(GameObject soundSource2d)
    {
        soundSource2d.GetComponentInChildren<AudioSource>().PlayOneShot(clickSound);
    }

    public void playAlertSound(GameObject soundSource2d)
    {
        soundSource2d.GetComponentInChildren<AudioSource>().PlayOneShot(alertSound);
    }

    public void playFailSound(GameObject soundSource2d)
    {
        soundSource2d.GetComponentInChildren<AudioSource>().PlayOneShot(failSound);
    }

    public void playClickSound(AudioSource soundSource2d)
    {
        soundSource2d.PlayOneShot(clickSound);
    }

    public void playAlertSound(AudioSource soundSource2d)
    {
        soundSource2d.PlayOneShot(alertSound);
    }

    public void playFailSound(AudioSource soundSource2d)
    {
        soundSource2d.PlayOneShot(failSound);
    }


    private void OnEnable()
    {
        startDelegate += dummyFunc;
        tickDelegate += dummyFunc;
            
    }

    public int CreateInstance(ScriptedUI newUI)
    {
        int index = ActiveInterfaces.IndexOf(newUI);
        if (index < 0){
            Debug.LogError("Interface "+newUI +" Not in active List");
            return -1;
        } 

        if (newUI.CanTick)
            {
                tickDelegate += newUI.Update;
            }  

        if (newUI.RunOnSceneLoad)
            {
                startDelegate += newUI.Start;
            }  
        return ActiveInterfaces[index].CreateUIInstance();
    }

    public void DestroyInstance(ScriptedUI uiToDestroy, int instanceId)
    {
        if (!ActiveInterfaces.Contains(uiToDestroy)) return;
        int index = ActiveInterfaces.IndexOf(uiToDestroy);
        if (index < 0) return;

         if (uiToDestroy.CanTick)
            {
                tickDelegate -= uiToDestroy.Update;
            }  

        if (uiToDestroy.RunOnSceneLoad)
            {
                startDelegate -= uiToDestroy.Start;
            }  
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
        linkedEventSystem = new GameObject("EventSystem", typeof(EventSystem), typeof(StandaloneInputModule));
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

    public RaycastHit CursorRaycast(Camera thisCamera,int layermask)
    {
        Ray ray = thisCamera.ScreenPointToRay(Input.mousePosition);
        
        RaycastHit hit;
        Physics.Raycast(ray, out hit, Mathf.Infinity, layermask);
        return hit;
    }

    public override void Reset()
    {
        
    }
}
