using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ConditionScript : ScriptableObject
{
    public abstract bool ConditionCheck(ScriptableObject callingObject);

}
