using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ResourceNode : ScriptableObject
{
    [SerializeField] public GameObject _linkedGameObject;
    [SerializeField] protected bool _simulateWhenUnloaded;
    [SerializeField] protected  ResourceManager _linkedResourceManager;
    [SerializeField] protected bool isActive = true;

    public bool IsActive{get => isActive;}

    public GameObject GetGameObject{get{return _linkedGameObject;}}

    public void Initalize(GameObject linkedGameObject,bool simulateWhenUnloaded, ResourceManager LinkedResourceManager)
    {
        _linkedGameObject = linkedGameObject;
        _simulateWhenUnloaded = simulateWhenUnloaded;
        _linkedResourceManager = LinkedResourceManager;
    }

    public void SetActive(bool newActive)
    {
        isActive = newActive;
    }

    public abstract int TryInput(short resourceType,int amount);

    public abstract int TryOutput(short resourceType,int amount);

    public abstract bool Run();
}
