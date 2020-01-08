using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Core/Debug/ScriptedActions/PickupObject")]
public class PickupObject : Action
{
    public override void ActionFunction(Pawn pawn)
   {
       Debug.Log(pawn + " Picked up an object");
   }
}
