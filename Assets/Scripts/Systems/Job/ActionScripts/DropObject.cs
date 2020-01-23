using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Core/Debug/ScriptedActions/DropObject")]
public class DropObject : Action
{
    public override void ActionFunction(Pawn pawn)
   {
       Debug.Log(pawn + " Dropped an object");

       // The Pawn will have to drop it's current content
       // The object will be instantiated in front of the last known location
       // the Pawn will only be able to drop an object if they die, or if they reach the designated building.
   }
}
