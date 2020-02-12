using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameFramework/SubSystems/ResourceModule")]
public class ResourceModule : Module
{
    [SerializeField] private List<Resource> ActiveResources = new List<Resource>();
    [SerializeField] private List<float> ResourceLimits = new List<float>();
    [SerializeField] private List<float> ResourceStarts = new List<float>();
    [SerializeField] private List<ResourceSink> ResourceSinks = new List<ResourceSink>();

    //holy crap this is inefficent, I should be using custom structs for the data
    private Dictionary<Resource,float> resourceStorage = new Dictionary<Resource,float>();
    private Dictionary<Resource,float> storageLimits = new Dictionary<Resource,float>();
    private Dictionary<Resource,float> resourceMultipliers = new Dictionary<Resource, float>();

    public List<ResourceBehavior> resourceNodes = new List<ResourceBehavior>();
    public List<ResourceBehavior> linkedResourceNodes = new List<ResourceBehavior>();

    public Dictionary<Resource,float> GetStorage{get =>resourceStorage;}
    public List<Resource> Resources{get =>ActiveResources;}
    

    private void OnEnable()
    {
        Reset();
        foreach (var sink in ResourceSinks)
        {
            sink.resourceModule = this;
        }
    }




    public override void Update()
    {
        if(Time.frameCount%60 <= 0)//every second
        {
            Debug.Log("Resource Network Update");
            // foreach (var item in resourceStorage)
            // {
            //     Debug.Log("Resource: "+ item.Key.name + " "+ item.Value);
            // }
            foreach (ResourceBehavior resourceNode in resourceNodes)
            {
                Debug.Log("Running: "+ resourceNode);
                if (resourceNode.resourceComponent.checkCondition(null,null))
                {
                    resourceNode.resourceComponent.Run();
                }
            }

            foreach (var sink in ResourceSinks)
            {
                sink.OnUpdate();
            }
        }
    }

    public float GetResourceModifier(Resource resource)
    {
        return resourceMultipliers[resource];
    }

    public void SetResourceMultiplier(Resource resource, float newMultiplier)
    {
        resourceMultipliers[resource] = newMultiplier;
    }

    public void AddResourceMultiplier(Resource resource, float addMultiplier)
    {   
        SetResourceMultiplier(resource,resourceMultipliers[resource]+= addMultiplier);
    }

    public void SubResourceMultiplier(Resource resource, float subMultiplier)
    {   
        SetResourceMultiplier(resource,resourceMultipliers[resource]-= subMultiplier);
    }

    public void RemoveResourceLimit(Resource resource, float amount)
    {
        Debug.Log(resource);
        storageLimits[resource] -= amount;
        
    }

    public void AddResourceLimit(Resource resource, float amount)
    {
        Debug.Log(resource);
        storageLimits[resource] += amount;
    }


    public void SetResourceLimit(Resource resource, float newLimit)
    {
        storageLimits[resource] = newLimit;
    }

    public float GetResourceLimit(Resource resource)
    {
        return storageLimits[resource];
    }

    public float GetResourceStorage(Resource resource)
    {
        return resourceStorage[resource];
    }


    private void tickResourceSystem()
    {
        foreach (ResourceBehavior curNode in linkedResourceNodes)
        {
           curNode.isActive = curNode.resourceComponent.checkCondition(curNode.gameObject,curNode);
           if (curNode.isActive) 
           {
               curNode.resourceComponent.Run();
           }
        }
    }

    public float AddResource(Resource resourceToAdd, float amount)
    {
        if (amount <= 0) return 0;

        float newStorage = GetStorage[resourceToAdd] + amount;
        if (newStorage > storageLimits[resourceToAdd])
        {
            resourceStorage[resourceToAdd] = storageLimits[resourceToAdd];
            return newStorage-storageLimits[resourceToAdd];
        }
        resourceStorage[resourceToAdd]  += amount;
        return 0;

    }

    public float RemoveResource(Resource resourceToRemove, float amount)
    {
        if (amount <= 0) return amount;
        if (GetStorage[resourceToRemove] >= amount){
            GetStorage[resourceToRemove] -= amount;
        }
        else 
        {
            return amount - GetStorage[resourceToRemove];
        }
        return amount;
    }


    public void SetResource(Resource resourceToSet, float amount)
    {
        resourceStorage[resourceToSet] = amount;
    }

    public override void Reset()
    {
        resourceStorage.Clear();
        resourceNodes.Clear();
        linkedResourceNodes.Clear();
        storageLimits.Clear();
        resourceMultipliers.Clear();

        for (int i = 0; i < ActiveResources.Count; i++)
        {
            resourceMultipliers.Add(ActiveResources[i],0.0f);
            resourceStorage.Add(ActiveResources[i],ResourceStarts[i]);
            storageLimits.Add(ActiveResources[i],ResourceLimits[i]);
        }
    }
}
