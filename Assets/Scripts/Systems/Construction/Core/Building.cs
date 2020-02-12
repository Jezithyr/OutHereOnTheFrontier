using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "BuildingSystem/Create New Building")]
public class Building : ScriptableObject
{
    [SerializeField] private GameObject prefab;
    public GameObject Prefab{get => prefab;}
    
    [SerializeField] private GameObject previewprefab;
    public GameObject Preview{get => previewprefab;}

    [SerializeField] private List<PlacementCondition> placementConditions= new List<PlacementCondition>();

    [SerializeField] private List<DestroyEffect> destroyEffects= new List<DestroyEffect>();

    public ConstructionModule constructionManager; 

    private List<GameObject> instances = new List<GameObject>();

    [SerializeField]
    private float buildingRadiusMultiplier = 1f;

    [SerializeField] public bool Removable = true;

    [SerializeField] public List<Resource> ResourceForCost;
    [SerializeField] public List<int> AmountForCost;

    [SerializeField] public string description;


    private float checkRadius;
    public float Radius{get=>checkRadius;}


    private void OnEnable()
    {
        checkRadius = buildingRadiusMultiplier*previewprefab.GetComponent<SphereCollider>().radius;
        instances.Clear();
    }

    public GameObject CreateInstance()
    {
        GameObject temp = GameObject.Instantiate(prefab);
        instances.Add(temp);
        return temp;
    }

    public bool IsPlaced()
    {
        return GetInstanceCount() > 0;
    }

    public int GetInstanceCount()
    {
        return instances.Count;
    }


    public void Deconstruct(GameObject instance)
    {
        GameObject temp = RemoveInstance(instance);
        foreach (var effect in destroyEffects)
        {
            effect.BuildingDestroyed(temp,true);
        }
        constructionManager.RemoveBuilding(temp);
    }

    private GameObject RemoveInstance(GameObject instance)
    {
        GameObject temp = instances.Find( x => x==instance);
        instances.Remove(temp);
        return temp;
    }

    public void DestroyInstance(GameObject instance)
    {
        GameObject temp = RemoveInstance(instance);
        foreach (var effect in destroyEffects)
        {
            effect.BuildingDestroyed(temp,false);
        }
        constructionManager.RemoveBuilding(temp);
    }

    public bool CheckPlacement(GameObject preview)
    {
        bool canPlace = true;
        foreach (var condition in placementConditions)
        {

            
            canPlace = canPlace &  condition.ConditionCheck(preview,this);
        }
        return canPlace;
    }
}
