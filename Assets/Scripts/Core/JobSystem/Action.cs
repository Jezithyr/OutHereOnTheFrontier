using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [!] Leaves this alone, Base class (Shell) for actions. Used for serialization [!]
// This is acting like a filter, which will only accept Actions.
public abstract class  Action : ScriptableObject
{
    public abstract void ActionFunction(Pawn pawn);
}
