using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceProducerComponent : ResourceSystemComponent
{

    protected Dictionary<short,int> ProductionsPerCycle = new Dictionary<short,int>();

    [SerializeField] private List<Resource> ResourceTypes;
    [SerializeField] private List<int> ProductionAmounts;
    [SerializeField] private int buffer = 50;

    private void ParseSettings()
    {
        if (ResourceTypes.Count != ProductionAmounts.Count)
        {
            throw new System.Exception(gameObject+"Has uneven property fields!");
        }

        for (int index = 0; index < ResourceTypes.Count; index++)
        {
            short resourceIndex = LinkedResourceManager.GetIndexFromResource(ResourceTypes[index]);
            ProductionsPerCycle.Add(resourceIndex,ProductionAmounts[index]);
        }
    }

   private new void Start()
   {
    base.Start();
    ParseSettings();
    ResourceProducer NewObject = ScriptableObject.CreateInstance<ResourceProducer>();
    _attachedNode = NewObject;
    NewObject.Initalize(gameObject,KeepLoaded,LinkedResourceManager,ProductionsPerCycle,buffer);
   }
}
