using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [!] Leaves this alone, Base class (Shell) for conditions. Used for serialization [!]
// This is acting like a filter, which will only accept Conditions.
public abstract class Condition : ScriptableObject
{
    public abstract byte CheckStatement(Pawn pawn);
}
