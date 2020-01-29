using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausedCondition : GameStateCondition
{
    public override bool ConditionCheck(GameState lastState)
    {
        return false;
    }
}
