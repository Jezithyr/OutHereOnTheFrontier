using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "BuildingSystem/Destroy Effects/Refund Resources")]
public class RefundCost : DestroyEffect
{
    [SerializeField]
    ResourceModule resourceController;

    [SerializeField] private List<Resource> Resources;
    [SerializeField] private List<short> amount;

    public override void BuildingDestroyed(GameObject building,bool demolished = false)
    {

        for (int i = 0; i < Resources.Count; i++)
        {
            resourceController.AddResource(Resources[i],amount[i]);
        }
    }
    
}
