using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameFramework/SubSystems/ResourceModule")]
public class ResourceModule : Module
{
    [SerializeField] 
    private List<Resource> ActiveResources = new List<Resource>();

    private Dictionary<Resource,short> resourceStorage = new Dictionary<Resource,short>();
    
    public Dictionary<Resource,short> GetStorage{get =>resourceStorage;}
    public List<Resource> Resources{get =>ActiveResources;}

    public List<ResourceBehavior> resourceNodes = new List<ResourceBehavior>();

    public List<ResourceBehavior> linkedResourceNodes = new List<ResourceBehavior>();


    //TODO: Optimize ticking system by moving stuff from monobehavior updates to centralized ticking 

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



    
}
