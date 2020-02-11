using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "EventSystem/Effects/Create New Resource Modifier")]
public class ResourceModifierEffect : EventEffect
{
    [SerializeField] private ResourceModule resourceModule;
    [SerializeField] private List<Resource> Resources;
    [SerializeField] private List<float> modifiers; //percentages

    public override void Run(ScriptableObject callingObject)
    {
        for (int i = 0; i < Resources.Count; i++)
        {
            resourceModule.AddResourceMultiplier(Resources[i],modifiers[i]);
        }
    }
}
