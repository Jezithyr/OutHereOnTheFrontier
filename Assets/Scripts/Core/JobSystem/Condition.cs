using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Condition : SingletonScriptableObject<Condition>
{
    public abstract byte CheckStatement(Pawn pawn);
}
