using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceBehavior : MonoBehaviour
{    
    

    [SerializeField] public ResourceComponent resourceComponent;

    public bool isActive = false;


    private bool emptydelegate(GameObject ownerObject, MonoBehaviour script)
    {
        return true;
    }

    private void OnEnable()
    {
        //initalize the delegates
 
        resourceComponent.resourceController.resourceNodes.Add(this);
        resourceComponent.instances.Add(this);
        
    }

    private void OnDestroy()
    {
        resourceComponent.instances.Remove(this);
    }

    private void Start()
    {
        resourceComponent.UpdateStorageLimits();
    }


}
