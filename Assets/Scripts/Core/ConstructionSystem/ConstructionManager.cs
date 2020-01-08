using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class ConstructionManager : ScriptableObject
{
    [SerializeField] private GridSystem linkedGrid;
    public GridSystem Grid {get => linkedGrid;}

    [SerializeField] private List<Building> EnabledBuildings = new List<Building>();

    [SerializeField] private Dictionary<GameObject,Building> ActiveBuildings = new Dictionary<GameObject,Building>() ;
    public Dictionary<GameObject,Building> Buildings{get => ActiveBuildings;}


    public Building GetDataForPrefab(GameObject prefab)
    {
        foreach (var entry in Buildings)
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
        foreach (var entry in Buildings)
        {
            if (entry.Value == buildingObject)
            {
                return entry.Key;
            }
        }
        return null;
    }


    public void CreateBuildingAt(Vector2Int gridPos, Quaternion rotation, Building buildingData)
    {
        CreateBuildingAt(linkedGrid.ConvertGridToWorldPos(gridPos),rotation,buildingData);
    }


    public void CreateBuildingAt(Vector3 position,Quaternion rotation, Building buildingData)
    {
        if (!EnabledBuildings.Contains(buildingData)) return; //don't create the building if it isn't enabled

        Building tempBuildData = ScriptableObject.Instantiate(buildingData);
        GameObject prefab = GameObject.Instantiate(tempBuildData.Prefab);
        
        prefab.transform.position = position;
        prefab.transform.rotation = rotation;
        
        ActiveBuildings.Add(prefab,tempBuildData);
    }


    public void RemoveBuilding(GameObject prefab)
    {
        foreach (var entry in Buildings)
        {
            Building buildingDataObj = entry.Value;
            GameObject prefabObj = entry.Key;

            if (prefabObj == prefab)
            {                
                Buildings.Remove(prefabObj); //this can be optimized
                Destroy(prefabObj);
                Destroy(buildingDataObj);
            }
        }
    }


    public void RemoveBuilding(Building buildingData)
    {
        foreach (var entry in Buildings)
        {
            Building buildingDataObj = entry.Value;
            GameObject prefabObj = entry.Key;

            if (entry.Value == buildingData)
            {
                Buildings.Remove(prefabObj); //this can be optimized
                Destroy(prefabObj);
                Destroy(buildingData);
            }
        }
    }
}
