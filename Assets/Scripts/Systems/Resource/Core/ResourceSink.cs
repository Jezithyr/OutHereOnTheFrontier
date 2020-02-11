using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ResourceSystem/Create New Resource Sink")]
public class ResourceSink : ScriptableObject
{
    [SerializeField] protected List<ConditionScript> checkConditions  = new List<ConditionScript>();
    [SerializeField] protected List<EventEffect> successEffects = new List<EventEffect>();
    [SerializeField] protected List<EventEffect> failEffect = new List<EventEffect>();
    protected bool success = true;
    public ResourceModule resourceModule;

    public void OnUpdate()
    {
        RunCheck();
    }

    protected virtual void RunCheck()
    {
        
        foreach (var check in checkConditions)
        {
            success = success && check.ConditionCheck(this);
        }

        Run();
    }

    protected virtual void Run()
    {

        if (success)
        {
            foreach (var effect in successEffects)
            {
                effect.Run(this);
            }
        }
        else 
        {
             foreach (var effect in failEffect)
            {
                effect.Run(this);
            }
        }
        
    }
}
