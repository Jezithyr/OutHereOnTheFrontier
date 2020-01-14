using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ResourceSystem/Create New Consumer")]
public class ResourceConsumer : ResourceComponent
{
    public override void Run()
    {
        for (int i = 0; i < activeResources.Count; i++)
        {
            resourceController.RemoveResource(activeResources[i],consumption[i]);
        }
    }

    public override bool checkCondition(GameObject gameObject, MonoBehaviour script)
    {
       for (int i = 0; i < activeResources.Count; i++)
        {
            if (resourceController.GetResourceStorage(activeResources[i]) <= 0)
            {
                return false;
            }
        }
       return true;
    }

}
