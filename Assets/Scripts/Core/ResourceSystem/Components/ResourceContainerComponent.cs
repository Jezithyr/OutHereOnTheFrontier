using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceContainerComponent : ResourceSystemComponent
{

    protected Dictionary<short,Vector2Int> ResourceStorage = new  Dictionary<short,Vector2Int>();

     [SerializeField] private List<Resource> ResourceTypes;
    [SerializeField] private List<int> ResourceMaximums;


   private new void Start()
   {
    base.Start();
    ParseSettings();
    ResourceContainer NewObject = ScriptableObject.CreateInstance<ResourceContainer>();
     _attachedNode = NewObject;
    NewObject.Initalize(gameObject,KeepLoaded,LinkedResourceManager,ResourceStorage);
    
   }

   
     private void ParseSettings()
    {
        if (ResourceTypes.Count != ResourceMaximums.Count)
        {
            throw new System.Exception(gameObject+"Has uneven property fields!");
        }

        for (int index = 0; index < ResourceTypes.Count; index++)
        {
            ResourceStorage.Add(LinkedResourceManager.GetIndexFromResource(ResourceTypes[index]),new Vector2Int(0,ResourceMaximums[index]));
        }
    }
}
