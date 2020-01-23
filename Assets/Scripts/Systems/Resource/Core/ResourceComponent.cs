using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//resource component base class

public class ResourceComponent : ScriptableObject
{
    [Header("Links")]
    [SerializeField] public ResourceModule resourceController;

    [Header("Resource System")]
    [SerializeField] protected List<Resource> activeResources = new List<Resource>();
    [SerializeField] protected List<short> production = new List<short>();
    [SerializeField] protected List<short> consumption = new List<short>();
    [SerializeField] protected List<short> storage = new List<short>();

    public virtual bool checkCondition(GameObject gameObject, MonoBehaviour script)
    {
        return true;
    }

    public virtual void Run()
    {

    }
}
