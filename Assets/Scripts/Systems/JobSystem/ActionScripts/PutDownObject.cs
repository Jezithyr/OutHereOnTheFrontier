using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Core/Debug/ScriptedActions/PutDownObject")]
public class PutDownObject : Action
{
    public override void ActionFunction(Pawn pawn)
   {
       Debug.Log(pawn + " Put down an object");
   }
}
