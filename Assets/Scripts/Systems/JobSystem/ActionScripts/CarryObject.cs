using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Core/Debug/ScriptedActions/CarryObject")]
public class CarryObject : Action
{
   public override void ActionFunction(Pawn pawn)
   {
       Debug.Log(pawn + " Is carrying an object");
   }
}
