using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : Item_Base
{
    [SerializeField] private Resource storedResource;
    [SerializeField] private short maxStorage = 10;
    [SerializeField] private short currentStorage = 0;

    public short Storage{get => currentStorage;}
    public Resource Resource{get => storedResource;}
}
