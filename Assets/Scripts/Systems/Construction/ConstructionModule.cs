﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(menuName = "GameFramework/SubSystems/ConstructionModule")]
public class ConstructionModule : Module
{
    [SerializeField] private GridSystem linkedGrid; //todo phase out grid system
    public GridSystem Grid {get => linkedGrid;}

    [SerializeField] private List<Building> EnabledBuildings = new List<Building>();
    public List<Building> enabledBuildings {get => EnabledBuildings;}

    [SerializeField] private Dictionary<GameObject,Building> ActiveBuildings = new Dictionary<GameObject,Building>() ;
    public Dictionary<GameObject,Building> Buildings{get => ActiveBuildings;}



    private void OnEnable()
    {
        foreach (var building in enabledBuildings)
        {
            building.constructionManager = this;
        }
    }


    public Building GetDataForPrefab(GameObject prefab)
    {
        Dictionary<GameObject,Building> BuildingsTemp = new Dictionary<GameObject,Building>(ActiveBuildings);

        foreach (var entry in BuildingsTemp)
        {
            if (entry.Key == prefab)
            {
                return entry.Value;
            }
        }
        return null;
    }


    public GameObject GetPrefabFromBuildingData(Building buildingObject)
    {
        Dictionary<GameObject,Building> BuildingsTemp = new Dictionary<GameObject,Building>(ActiveBuildings);

        foreach (var entry in BuildingsTemp)
        {
            if (entry.Value == buildingObject)
            {
                return entry.Key;
            }
        }
        return null;
    }


    public void CreateBuildingAtGridPos(Vector2Int gridPos, Quaternion rotation, Building buildingData)
    {
        CreateBuildingAtWorldPos(linkedGrid.ConvertGridToWorldPos(gridPos),rotation,buildingData);
    }


    public void CreateBuildingAtWorldPos(Vector3 position,Quaternion rotation, Building buildingData)
    {
        if (!EnabledBuildings.Contains(buildingData)) 
        {
            Debug.LogError("WARNING: Building"+ buildingData +" Not found in active building list!");
            return; //don't create the building if it isn't enabled
        }

        GameObject prefab = buildingData.CreateInstance();
        
        prefab.transform.position = position;
        prefab.transform.rotation = rotation;
        
        ActiveBuildings.Add(prefab,buildingData);
    }


    public bool RemoveBuilding(GameObject prefab)
    {
        Dictionary<GameObject,Building> BuildingsTemp = new Dictionary<GameObject,Building>(ActiveBuildings);

        foreach (var entry in BuildingsTemp)
        {
            Building buildingDataObj = entry.Value;
            GameObject prefabObj = entry.Key;

            if (prefabObj == prefab)
            {                
                Buildings.Remove(prefabObj); //this can be optimized
                DestroyImmediate(prefabObj,true);
                return true;
            }
        }
        return false;
    }

    public bool BuildingIsEnabled(Building buildingData)
    {
        return enabledBuildings.Contains(buildingData);
    }

    public void RemoveBuilding(Building buildingData)
    {
        Dictionary<GameObject,Building> BuildingsTemp = new Dictionary<GameObject,Building>(ActiveBuildings);

        foreach (var entry in BuildingsTemp)
        {
            Building buildingDataObj = entry.Value;
            GameObject prefabObj = entry.Key;

            if (entry.Value == buildingData)
            {
                Buildings.Remove(prefabObj); //this can be optimized
                DestroyImmediate(prefabObj);
            }
        }
    }

    public GameObject CreatePreviewAtPos(Building buildingObj, Vector3 position)
    {
        return GameObject.Instantiate(buildingObj.Preview,position,new Quaternion());
    }

    public GameObject CreatePreviewWithTransform(Building buildingObj, Transform transform)
    {
        return GameObject.Instantiate(buildingObj.Preview,transform);
    }

    public override void Reset()
    {
        Dictionary<GameObject,Building> BuildingsTemp = new Dictionary<GameObject,Building>(ActiveBuildings);

        foreach (var entry in BuildingsTemp)
        {
            RemoveBuilding(entry.Value);
        }
    }
}
