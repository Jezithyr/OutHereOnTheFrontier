using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceConsumerComponent : ResourceSystemComponent
{

   protected Dictionary<short,int> ConsumptionsPerCycle;

   [SerializeField] private List<Resource> ResourceTypes;
    [SerializeField] private List<int> ConsumptionAmounts;
    [SerializeField] private int buffer = 50;





   private new void Start()
   {
   base.Start();
   ParseSettings();
    ResourceConsumer NewObject = ScriptableObject.CreateInstance<ResourceConsumer>();
    _attachedNode = NewObject;
    NewObject.Initalize(gameObject,KeepLoaded,LinkedResourceManager,ConsumptionsPerCycle,buffer);
    
   }


     private void ParseSettings()
    {
        if (ResourceTypes.Count != ConsumptionAmounts.Count)
        {
            throw new System.Exception(gameObject+"Has uneven property fields!");
        }

        for (int index = 0; index < ResourceTypes.Count; index++)
        {
            ConsumptionsPerCycle.Add(LinkedResourceManager.GetIndexFromResource(ResourceTypes[index]),ConsumptionAmounts[index]);
        }
    }

}
