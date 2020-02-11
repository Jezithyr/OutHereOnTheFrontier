using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//[CreateAssetMenu(menuName = "BuildingSystem/Destroy Effects/")]
public abstract class DestroyEffect : ScriptableObject
{
    public abstract void BuildingDestroyed(GameObject building,bool demolished = false);
}
