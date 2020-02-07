using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "EventSystem/Effects/New Fill Requirement")]
public class FillReqEffect : EventEffect
{
    private bool triggered = false;
    public bool Triggered{get => triggered;}

    public override void Run(ScriptableObject callingObject)
    {
        triggered = true;
    }
}
