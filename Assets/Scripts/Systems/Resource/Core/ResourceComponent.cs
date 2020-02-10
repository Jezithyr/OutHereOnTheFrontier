using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//resource component base class


[CreateAssetMenu(menuName = "ResourceSystem/Create New Component")]
public class ResourceComponent : ScriptableObject
{

    delegate void RunDelegate();
    delegate bool CheckConditionDelegate(GameObject gameObject, MonoBehaviour script);
    private RunDelegate runDelegate;
    private CheckConditionDelegate checkProducerConditionDelegate;
    private CheckConditionDelegate checkConsumerConditionDelegate;
    public List<ResourceBehavior> instances = new List<ResourceBehavior>();


    [Header("Links")]
    [SerializeField] public ResourceModule resourceController;

    [Header("Resource System")]
    [SerializeField] protected List<Resource> activeResources = new List<Resource>();
    [SerializeField] protected List<float> production = new List<float>();
    [SerializeField] protected List<float> consumption = new List<float>();
    [SerializeField] protected List<float> storage = new List<float>();

    private void dummyfunc(){}

    private bool dummyfunc(GameObject temp, MonoBehaviour script){return true;}

    private void OnEnable()
    {
        instances.Clear();

        runDelegate += dummyfunc;

        if (consumption.Count > 0)
        {
            runDelegate += ConsumerRun;
            checkConsumerConditionDelegate = ConsumerCheckCondition;
        }
        else
        {
            checkConsumerConditionDelegate = dummyfunc;
        }

        if (production.Count > 0)
        {
      
            runDelegate += ProducerRun;
            checkProducerConditionDelegate = ProducerCheckCondition;
        }
        else
        {
            checkProducerConditionDelegate = dummyfunc;
        }
    }

    public void UpdateStorageLimits()
    {
        if (storage.Count == 0) return;

        for (int i = 0; i < activeResources.Count; i++)
        {
            resourceController.SetResourceLimit(activeResources[i],resourceController.GetResourceLimit(activeResources[i]) + storage[i]);
        }
    }


    public bool checkCondition(GameObject gameObject, MonoBehaviour script)
    {
        return checkConsumerConditionDelegate(gameObject,script) & checkProducerConditionDelegate(gameObject,script);
    }

    public void Run()
    {
        if (storage.Count > 0)
        {
        }
        runDelegate();
    }




    private void ProducerRun()
    {
        for (int i = 0; i < activeResources.Count; i++)
        {

            resourceController.AddResource(activeResources[i],production[i]*(1+resourceController.GetResourceModifier(activeResources[i])));
        }
    }

    private bool ProducerCheckCondition(GameObject gameObject, MonoBehaviour script)
    {
        for (int i = 0; i < activeResources.Count; i++)
        {
            if (resourceController.GetResourceStorage(activeResources[i]) == resourceController.GetResourceLimit(activeResources[i]))
            {
                return false;
            }
        }
       return true;
    }

    public void ConsumerRun()
    {
        for (int i = 0; i < activeResources.Count; i++)
        {
            resourceController.RemoveResource(activeResources[i],consumption[i]*(1+resourceController.GetResourceModifier(activeResources[i])));
        }
    }

    public bool ConsumerCheckCondition(GameObject gameObject, MonoBehaviour script)
    {
        bool temp = true;
       for (int i = 0; i < activeResources.Count; i++)
        {
            temp = temp & resourceController.GetResourceStorage(activeResources[i]) >= consumption[i];
        }
       return temp;
    }


}
