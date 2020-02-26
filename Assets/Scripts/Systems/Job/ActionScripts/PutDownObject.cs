using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Core/Debug/ScriptedActions/PutDownObject")]
public class PutDownObject : Action
{
    public override void ActionFunction(Pawn pawn)
   {
       Debug.Log(pawn + " Put down an object");

       // Have the object the pawn is carrying, have a spot in the storage component
       // Do a check to ensure that the pawn entering is holding anything or not.
       // On entry of the building, the Pawn will drop the item it has been holding. 
       // Make sure the items cannot collide when storing them in their designated building. 
   }
}
