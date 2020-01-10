using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName= "temp2")]
public class DebugStateCondition : GameStateCondition
{
    public override bool ConditionCheck(GameState lastState)
    {
        return true;
    }
}
