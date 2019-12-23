using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName= "Core/ResourceSystem/Resource")]
public class Resource : ScriptableObject
{
    [SerializeField] protected Type ResourceType;
    [SerializeField] protected string DisplayName;
    [SerializeField] protected Sprite Icon;
    [SerializeField] protected string ToolTip;

    [SerializeField] protected int _volume,_mass,_value;
    [SerializeField] protected bool _physical,_liquid;

    public int Volume {get{return _volume;}}
   
    public int Mass {get{return _mass;}}
    
    public int Value {get{return _value;}}

    public bool IsPhysical {get{return _physical;}}
    public bool IsLiquid {get{return _liquid;}}
}
