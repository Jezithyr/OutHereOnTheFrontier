using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceComponent : ScriptableObject
{
    [Header("Links")]
    [SerializeField] private ResourceModule resourceController;

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
