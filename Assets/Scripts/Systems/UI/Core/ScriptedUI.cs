using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptedUI : ScriptableObject
{
    [SerializeField] 
    protected UIModule uiController;

    [SerializeField] public bool CanTick = false;
    [SerializeField] public bool RunOnSceneLoad = false;

    [SerializeField] protected bool isActive = false;

    public bool IsActive{get =>isActive;}


    [SerializeField] 
    protected GameObject UiPrefab;

    public GameObject Prefab{get =>UiPrefab;}

    protected List<ScriptedUIBehavior> linkedBehaviorScripts = new List<ScriptedUIBehavior>();

    protected bool started = false;

    //gets the linked ui IF there is only 1 instance of the ui, and that ui is initalized
    protected ScriptedUIBehavior linkedUI{get => linkedBehaviorScripts[0];} 


    protected virtual void Initialize()
    {

    }

    private void OnEnable()
    {
        started = false;
        ClearBehaviors();
        Initialize();
    }


    public virtual void Start(){}


    public virtual void Update(){}

    public void ClearBehaviors()
    {
        linkedBehaviorScripts.Clear();
    }


    public virtual int CreateUIInstance()
    {
        if (UiPrefab == null) return -1;

        Debug.Log("UI CREATED");

        GameObject newUIObj = GameObject.Instantiate(UiPrefab);
        ScriptedUIBehavior temp = newUIObj.GetComponentInChildren<ScriptedUIBehavior>();

        if (!temp) return -1;
        
        linkedBehaviorScripts.Add(temp);
        Start();
        started = true;
        return linkedBehaviorScripts.Count - 1;
    }

    public virtual void DestroyInstance(int instanceId)
    {
        if (instanceId >= linkedBehaviorScripts.Count) return;

        GameObject temp = linkedBehaviorScripts[instanceId].gameObject;
        linkedBehaviorScripts.RemoveAt(instanceId);
        Destroy(temp);
    }

    public void ToggleUI(int instanceId, bool newState)
    {
        if (instanceId >= linkedBehaviorScripts.Count) return;
        linkedBehaviorScripts[instanceId].gameObject.SetActive(newState);
    }

}
