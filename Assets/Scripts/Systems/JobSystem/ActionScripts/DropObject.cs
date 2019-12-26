using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Core/Debug/ScriptedActions/DropObject")]
public class DropObject : Action
{
    public override void ActionFunction(Pawn pawn)
   {
       Debug.Log(pawn + " Dropped an object");
   }
}
