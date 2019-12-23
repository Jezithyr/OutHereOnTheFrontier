using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSystemComponent : MonoBehaviour
{
    protected ResourceManager LinkedResourceManager;
    [SerializeField] protected bool KeepLoaded;
    
    protected ResourceNode _attachedNode;
    [SerializeField]
    protected bool _isActive = true;
    public ResourceNode AttachedNode{get => _attachedNode;}


    public void SetActive(bool newActive)
    {
        _isActive = newActive;
        _attachedNode.SetActive(newActive);
    }

    protected void Update()
    {
    }

    protected void Start()
    {
        LinkedResourceManager = ResourceManager.Instance;
        if (LinkedResourceManager == null)
        {
            throw new System.Exception(gameObject+" Does not have an assigned Resource Controller. This is bad, you should fix it...");
        }
    }
}
