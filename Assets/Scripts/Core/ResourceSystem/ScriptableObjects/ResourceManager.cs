using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName= "Core/ResourceSystem/ResourceManager")]
public class ResourceManager : SingletonScriptableObject<ResourceManager>
{
    [SerializeField] private List<Resource> ResourceList;
    private Dictionary<short,Resource> ResourceMapping = new Dictionary<short,Resource>();
    private Dictionary<Resource,short> ReverseResourceMapping = new Dictionary<Resource,short>();


    private List<ResourceLink> NetworkProducerLinks;
    private List<ResourceLink> NetworkContainerLinks;
    private List<ResourceLink> NetworkConsumerLinks;
    private bool isInitalized = false;





    public void AddLink(ResourceLink newLink)
    {
        switch(newLink.InputNode)
        {
            case ResourceProducer _producer:
            {
                if (!NetworkProducerLinks.Contains(newLink)) NetworkProducerLinks.Add(newLink);
                break;
            }
            case ResourceConsumer _consumer:
            {
                if (!NetworkConsumerLinks.Contains(newLink)) NetworkConsumerLinks.Add(newLink);
                break;
            }
            case ResourceContainer _container:
            {
                if (!NetworkContainerLinks.Contains(newLink)) NetworkContainerLinks.Add(newLink);
                break;
            }
            default:
            {
                break;
            }
        }


    }



    private void InitalizeResources()
    {
        ResourceMapping.Clear();
        ReverseResourceMapping.Clear();
    for (short index = 0; index < ResourceList.Count; index++)
    {
        Resource temp = ResourceList[index];
        ResourceMapping.Add(index,temp);
        ReverseResourceMapping.Add(temp,index);
    }
    }

    public Resource GetResourceFromIndex(short resourceIndex)
    { 
       Resource returnValue = null;
       if(! ResourceMapping.TryGetValue(resourceIndex,out returnValue))
       {
           throw new System.Exception("Tried to get a resource from a nonexistant index, or uninitalized resourcelist");
       }

        return returnValue;
    }

    public short GetIndexFromResource(Resource resourceIn)
    {
        short returnValue = -1;
        if(! ReverseResourceMapping.TryGetValue(resourceIn,out returnValue))
       {
           throw new System.Exception("Tried to get an index from a nonexistant resource, or uninitalized resourcelist. Resource = "+resourceIn);
       }
        return returnValue;
    }

    private void OnEnable()
    {
        InitalizeResources();
    }

private void networkUpdate()
{
    UpdateProducers();
    UpdateContainers();
    UpdateConsumers();
}


    private void UpdateProducers()
    {
        foreach (ResourceLink currentLink in NetworkProducerLinks)
        {
            if (currentLink.InputNode.IsActive){
            foreach (short resourceID in  currentLink.Resources)
            {
                
                currentLink.TryTransfer(resourceID,((ResourceProducer)currentLink.InputNode).GetProductionPerCycle(resourceID));
            }
            }
        }
    }

    private void UpdateConsumers()
    {
        foreach (ResourceLink currentLink in NetworkConsumerLinks)
        {
            if (currentLink.InputNode.IsActive){
            foreach (short resourceID in  currentLink.Resources)
            {
                currentLink.TryTransfer(resourceID,((ResourceConsumer)currentLink.InputNode).GetConsumptionPerCycle(resourceID));
            }
            }
        }
    }

     private void UpdateContainers()
    {
        foreach (ResourceLink currentLink in NetworkContainerLinks)
        {
            if (currentLink.InputNode.IsActive){
            foreach (short resourceID in  currentLink.Resources)
            {
                currentLink.TryTransfer(resourceID,((ResourceConsumer)currentLink.OutputNode).GetConsumptionPerCycle(resourceID));
            }
            }
        }
    }

}
