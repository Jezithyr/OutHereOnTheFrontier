using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Base : ScriptableObject
{
    private string _displayText = "";
    private bool _physicalized = true;
    private bool _simulatePhysics = false;
    private bool _effectsPathing = false;
    
    private GameObject attachedObject;


    [SerializeField] private Transform Location;
    [SerializeField] private GameObject Prefab;


}
