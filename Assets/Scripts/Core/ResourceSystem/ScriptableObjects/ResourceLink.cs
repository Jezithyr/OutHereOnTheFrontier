using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName= "Debug/ResourceSystem/CreateLink")]
public class ResourceLink : ResourceNodeVirtual
{
    [SerializeField] protected ResourceNode _inputNode;
    [SerializeField] protected ResourceNode _outputNode;
    [SerializeField] protected List<short> _resources;
    [SerializeField] private ResourceManager _resourceController;

    public ResourceNode InputNode{get{return _inputNode;}}
    public ResourceNode OutputNode{get{return _outputNode;}}
    public List<short> Resources{get{return _resources;}}

    public void TryTransfer(short resourceType,int amount)
    {
        ResourceNode origin;
        ResourceNode endpoint;

        if (amount> 0)
        {
            origin = _inputNode;
            endpoint = _outputNode;

        }else{
            origin = _inputNode;
            endpoint = _outputNode;

        }
        
        int transferAmount = origin.TryOutput(resourceType,amount);
        endpoint.TryInput(resourceType,transferAmount);
        Debug.Log("Debug: Transfer "+ transferAmount);
        
    }


    public void Initalize(ResourceNode _inputNode, ResourceNode _outputNode, List<Resource> resources)
    {
        if ((_inputNode)&(_outputNode))//nullcheck on the input and output nodes
        {

        _resourceController = ResourceManager.Instance;
        foreach (Resource item in resources)
        {
            _resources.Add(_resourceController.GetIndexFromResource(item));
        }
        switch(_inputNode)
        {
            case ResourceProducer _producer:
            {
                _resourceController.AddLink(this);
                break;
            }
            case ResourceConsumer _consumer:
            {
                _resourceController.AddLink(this);
                break;
            }
            case ResourceContainer _container:
            {
                _resourceController.AddLink(this);
                break;
            }
            default:
            {
                break;
            }
        }

    }
    return;


    }
}
