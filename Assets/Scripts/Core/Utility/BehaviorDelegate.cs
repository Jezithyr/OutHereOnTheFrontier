using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//baseclass for behavior delegates

public abstract class BehaviorDelegate : ScriptableObject
{
    delegate bool BehaviorMultiDelegate(GameObject ownerObject, MonoBehaviour script); 

    public abstract bool runDelegate(GameObject ownerObject, MonoBehaviour script);
}
