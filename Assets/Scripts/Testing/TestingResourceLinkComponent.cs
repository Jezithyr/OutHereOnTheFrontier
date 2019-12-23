using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingResourceLinkComponent : MonoBehaviour
{
    protected ResourceManager LinkedResourceManager;
    [SerializeField] private ResourceSystemComponent _inputNode;
     [SerializeField] private ResourceSystemComponent _outputNode;
    [SerializeField] private List<Resource> resourceTypes;
    private ResourceNodeVirtual _attachedNode;
    

    private void Start()
   {
    LinkedResourceManager = ResourceManager.Instance;
    if (LinkedResourceManager == null)
    {
        throw new System.Exception(gameObject+" Does not have an assigned Resource Controller. This is bad, you should fix it...");
    }
    ResourceLink NewObject = ScriptableObject.CreateInstance<ResourceLink>();
    _attachedNode = NewObject;
    NewObject.Initalize(_inputNode.AttachedNode,_outputNode.AttachedNode,resourceTypes);

   }
}
