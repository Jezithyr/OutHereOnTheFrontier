using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "GameFramework/Conditions/Pop growth condition")]
public class PopulationGrowthCondition : ResourceConditon
{
    
    [SerializeField] public List<Resource> consumedResources = new List<Resource>();
    [SerializeField] public List<float> resourcePerPop = new List<float>();

    [SerializeField] public Resource populationResource;


    public override bool ConditionCheck(ScriptableObject callingObject)
    {
        bool isGrowing = true;
        float population = Mathf.Floor(resourceModule.GetResourceStorage(populationResource));

        for (int i = 0; i < consumedResources.Count; i++)
        {
            isGrowing = isGrowing && resourceModule.GetResourceStorage(consumedResources[i]) >= (resourcePerPop[i] * population);
        }
        return isGrowing;
    }


}
