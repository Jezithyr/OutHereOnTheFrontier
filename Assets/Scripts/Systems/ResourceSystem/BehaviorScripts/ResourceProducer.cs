using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(menuName = "ResourceSystem/Create New Producer")]
public class ResourceProducer : ResourceComponent
{
    public override void Run()
    {
        for (int i = 0; i < activeResources.Count; i++)
        {
            resourceController.AddResource(activeResources[i],production[i]);
        }
    }


    public override bool checkCondition(GameObject gameObject, MonoBehaviour script)
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
}
