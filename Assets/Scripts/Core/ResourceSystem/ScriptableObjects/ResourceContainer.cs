using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName= "Debug/ResourceSystem/CreateContainer")]
public class ResourceContainer : ResourceNode
{
    [SerializeField] protected Dictionary<short,Vector2Int> StoredResources;



    
   public void Initalize(GameObject gameObject,bool KeepLoaded,ResourceManager LinkedResourceManager,Dictionary<short,Vector2Int> resourceStorage)
   {
       _linkedGameObject = gameObject;
      _simulateWhenUnloaded = KeepLoaded;
      _linkedResourceManager = LinkedResourceManager;
      StoredResources = resourceStorage;
   }



    public bool HasSpace(short ResourceIndex)
    {
        return (StoredResources[ResourceIndex].x < StoredResources[ResourceIndex].y);
    }

    public override int TryInput(short ResouceIndex,int InputAmount)
    {

        if ( !isActive) return 0;
        
        Debug.Log("Storing: "+ InputAmount+" of resource "+ResouceIndex);


        int overflow = StoredResources[ResouceIndex].x + InputAmount - StoredResources[ResouceIndex].y;
        

        if (overflow > 0)
        {
            StoredResources[ResouceIndex] = new Vector2Int(StoredResources[ResouceIndex].y,StoredResources[ResouceIndex].y);
            Debug.Log("overflow");
            return overflow;
            
        }
        else {
            StoredResources[ResouceIndex] = new Vector2Int(StoredResources[ResouceIndex].x +InputAmount,StoredResources[ResouceIndex].y);
        }
        
        return 0;
    }

    public override int TryOutput(short ResouceIndex,int InputAmount)
    {
        if ( !isActive) return 0;
        Debug.Log("UnStoring: "+ InputAmount+" of resource "+ResouceIndex);

        Vector2Int ResourceData = StoredResources[ResouceIndex];

        int missingUnits = StoredResources[ResouceIndex].x - InputAmount;

        if (missingUnits < 0)
        {
            StoredResources[ResouceIndex]  = new Vector2Int(0,StoredResources[ResouceIndex].y);
            return missingUnits;
        }else{
            StoredResources[ResouceIndex]  = new Vector2Int(StoredResources[ResouceIndex].x-InputAmount,StoredResources[ResouceIndex].y);
            return InputAmount;
        }
    }

    public override bool Run()
    {
        return true;
    }
}
