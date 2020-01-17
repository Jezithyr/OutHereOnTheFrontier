using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptedUI : ScriptableObject
{
    [SerializeField] 
    protected UIModule uiController;

    [SerializeField] public bool CanTick = false;
    [SerializeField] public bool RunOnSceneLoad = false;

    [SerializeField] 
    protected GameObject UiPrefab;

    public GameObject Prefab{get =>UiPrefab;}

    protected List<ScriptedUIBehavior> linkedBehaviorScripts = new List<ScriptedUIBehavior>();


    private void OnEnable()
    {
        
    }

    public virtual void Start(){}


    public virtual void Update(){}

    public void ClearBehaviors()
    {
        linkedBehaviorScripts.Clear();
    }

    public int CreateUIInstance()
    {
        if (UiPrefab == null) return -1;

        Debug.Log("UI CREATED");

        GameObject newUIObj = GameObject.Instantiate(UiPrefab);
        ScriptedUIBehavior temp = newUIObj.GetComponentInChildren<ScriptedUIBehavior>();

        if (!temp) return -1;
        
        linkedBehaviorScripts.Add(temp);
        return linkedBehaviorScripts.Count - 1;
    }

    public void DestroyInstance(int instanceId)
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
