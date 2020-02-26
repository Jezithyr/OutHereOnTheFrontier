
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EventSystem/Effects/New Resource Reward")]
public class ResourceReward : EventEffect
{
    [SerializeField] private List<Resource> resourceList = new List<Resource>();
    [SerializeField] private List<int> resourceRewards = new List<int>();
    [SerializeField] private ModuleManager moduleManager;

    public override void Run(ScriptableObject scriptObj)
    {
        ResourceModule resourceController = moduleManager.GetModule<ResourceModule>();
        
        for (int i = 0; i < resourceList.Count; i++)
        {
            resourceController.AddResource(resourceList[i],resourceRewards[i]);
        }
    }
}
