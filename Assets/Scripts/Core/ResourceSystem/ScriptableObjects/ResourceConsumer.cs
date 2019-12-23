using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName= "Debug/ResourceSystem/CreateConsumer")]
public class ResourceConsumer : ResourceNode
{
   [SerializeField] protected Dictionary<short,int> ConsumptionsPerCycle;
   [SerializeField] private int BufferSize = 10;
   [SerializeField] private bool startFull = false;

   private Dictionary<short,int> _resourceBuffer;

   public int GetConsumptionPerCycle(short resourceID)
    {
        return ConsumptionsPerCycle[resourceID];
    }

   public void Awake()
   {

   }

   public void Initalize(GameObject gameObject,bool KeepLoaded,ResourceManager LinkedResourceManager,Dictionary<short,int> ConsumptionRates, int buffer)
   {
      BufferSize = buffer;
      _linkedGameObject = gameObject;
      _simulateWhenUnloaded = KeepLoaded;
      _linkedResourceManager = LinkedResourceManager;
      ConsumptionsPerCycle = ConsumptionRates;

      foreach(KeyValuePair<short, int> consumptionData in ConsumptionsPerCycle)
      {
         _resourceBuffer.Add(consumptionData.Key,0);
      }

   }


   public bool BufferFull(short resourceID)
   {
      return _resourceBuffer[resourceID] >= BufferSize;
   }

   public bool BufferEmpty(short resourceID)
   {
      return _resourceBuffer[resourceID] == BufferSize;
   }

   public override bool Run()
   {      

      
      foreach(KeyValuePair<short, int> consumptionData in ConsumptionsPerCycle)
      {
         int _newResourceBufferValue = _resourceBuffer[consumptionData.Key] - consumptionData.Value;

         if (_newResourceBufferValue < 0) return false;

         _resourceBuffer[consumptionData.Key] = _newResourceBufferValue;
      }
      return true;
   }


   public override int TryInput(short resourceType,int amount)
   {
      Debug.Log("Nomming: "+ amount+" of resource "+resourceType);
      
      int excess;
      if (ConsumptionsPerCycle.ContainsKey(resourceType) && isActive && !BufferFull(resourceType))
      {
         excess = BufferSize-(_resourceBuffer[resourceType]+amount);
         return -excess;
      }else {
         return amount;
      }
   }





   public override int TryOutput(short resourceType, int amount)
   {
      return 0;
   }

}
