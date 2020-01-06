using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class  Action : SingletonScriptableObject<Action>
{
    public abstract void ActionFunction(Pawn pawn);
}
