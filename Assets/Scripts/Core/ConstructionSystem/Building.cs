using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : ScriptableObject
{
    [SerializeField] private GameObject prefab;
    public GameObject Prefab{get => prefab;}
    
    private ConstructionManager constructionManager; 

}
