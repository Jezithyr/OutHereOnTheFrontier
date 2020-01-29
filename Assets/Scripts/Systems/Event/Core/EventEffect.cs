using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//base class for event effects

public abstract class EventEffect : ScriptableObject
{
    public abstract void Run(ScriptableObject callingObject);
}