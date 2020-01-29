using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Core/Debug/ScriptedActions/CarryObject")]
public class CarryObject : Action
{
   public override void ActionFunction(Pawn pawn)
   {
       Debug.Log(pawn + " Is carrying an object");     

       // The pawn will need to be checked if it contains an object or not
       // The object the pawn is carrying will be floating in front of the model
       
   }
}
