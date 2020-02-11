using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ResourceSystem/Create New Population Sink")]
public class PopulationSink : ResourceSink
{
    private PopulationGrowthCondition popGrowthCondition;
    [SerializeField] private float popGrowthAmount = 0.05f;
    [SerializeField] private float popGrowthMultiplier = 0.0f;
    [SerializeField] private float popDeath = 0.1f;
    private void OnEnable()
    {
        popGrowthCondition = (PopulationGrowthCondition)checkConditions[0];
    }

    protected override void RunCheck()
    {
        if (popGrowthCondition.ConditionCheck(this))
        {
            Run();
        }
        else 
        {
            resourceModule.RemoveResource(popGrowthCondition.populationResource,popDeath);
        }

    }

    protected override void Run()
    {
        for (int i = 0; i < popGrowthCondition.consumedResources.Count; i++)
        {
            resourceModule.RemoveResource(popGrowthCondition.consumedResources[i],popGrowthCondition.resourcePerPop[i]);
        }   
        resourceModule.AddResource(popGrowthCondition.populationResource,popGrowthAmount*(1+popGrowthMultiplier));
    }
}
