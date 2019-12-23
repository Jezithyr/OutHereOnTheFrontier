using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName= "Debug/ResourceSystem/CreateProducer")]
public class ResourceProducer : ResourceNode
{
    [SerializeField] protected Dictionary<short,int> ProductionPerCycle;
    [SerializeField] private int BufferSize = 10;
    [SerializeField] private bool startFull = false;

    private Dictionary<short,int> _resourceBuffer = new Dictionary<short,int> ();


    public int GetProductionPerCycle(short resourceID)
    {
        return ProductionPerCycle[resourceID];
    }

    public void Awake()
   {
      
   }

   public void Initalize(GameObject gameObject,bool KeepLoaded,ResourceManager LinkedResourceManager,Dictionary<short,int> ProductionRates,int buffer)
   {
       BufferSize = buffer;
       _linkedGameObject = gameObject;
      _simulateWhenUnloaded = KeepLoaded;
      _linkedResourceManager = LinkedResourceManager;
      ProductionPerCycle = ProductionRates;


      foreach(KeyValuePair<short, int> productionData in ProductionPerCycle)
      {
        _resourceBuffer.Add(productionData.Key,0);
      }
   }
    


    public override bool Run()
    {
        bool success = false;
        foreach(KeyValuePair<short, int> productionData in ProductionPerCycle)
        {
            if (!BufferFull(productionData.Key))
            {
                
                _resourceBuffer[productionData.Key] += productionData.Value;
                success = true;
            }
        }
        return success;
    }



   public override int TryInput(short resourceType,int amount)
   {
      return amount;
   }

    public bool BufferFull(short resourceID)
   {
      return _resourceBuffer[resourceID] >= BufferSize;
   }

    public bool BufferEmpty(short resourceID)
   {
      return _resourceBuffer[resourceID] == BufferSize;
   }

    public int TryOutput(short resourceType)
    {
        if (ProductionPerCycle.ContainsKey(resourceType) && isActive && !BufferEmpty(resourceType))
        {
            int _newBufferValue = _resourceBuffer[resourceType] -ProductionPerCycle[resourceType];

            if (_newBufferValue < 0)
            {
                _resourceBuffer[resourceType] = 0;
                Debug.Log("Transfering: "+ ProductionPerCycle[resourceType]+_newBufferValue +" of resource "+resourceType+" out of buffer");
                return ProductionPerCycle[resourceType]+_newBufferValue;

            }

            Debug.Log("Transfering: "+ ProductionPerCycle[resourceType] +" of resource "+resourceType+" out of buffer");
            _resourceBuffer[resourceType] = _newBufferValue;
            return ProductionPerCycle[resourceType];

        }
        return 0;
    }



   public override int TryOutput(short resourceType, int amount)
   {
       return TryOutput(resourceType);


   /*   if (ProducedResources.ContainsKey(resourceType) && isActive)
      {
          int productionRate =  ProductionPerCycle[ProducedResources.IndexOf(resourceType)];
          
          if (productionRate > amount)
          {
               Debug.Log("Producing: "+ productionRate+" of resource "+resourceType+" Adjusted");
              return productionRate;
          }else {
               Debug.Log("Producing: "+ amount+" of resource "+resourceType);
              return amount;
          }
      }else {
          return 0;
      }*/
   }
}
