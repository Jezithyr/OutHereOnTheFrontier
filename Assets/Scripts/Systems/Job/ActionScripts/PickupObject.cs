using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Core/Debug/ScriptedActions/PickupObject")]
public class PickupObject : Action
{
    public override void ActionFunction(Pawn pawn)
   {
       Debug.Log(pawn + " Picked up an object");

       // The Pawn will need to collide with a resource object.
       // Destroy the static object, and instantiate it in front of the model
       // Keep the picked up object in front of the model, and make sure it cannot clip into the other models.

   }
}
