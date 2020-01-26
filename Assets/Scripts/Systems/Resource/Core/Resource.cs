using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ResourceSystem/Create New Resource")]
public class Resource : ScriptableObject
{
    [SerializeField]
    public string resourceName = "";
    
    [SerializeField] 
    public short volume;
    
    [SerializeField] 
    public short mass;

    [SerializeField] 
    public float value;
}
