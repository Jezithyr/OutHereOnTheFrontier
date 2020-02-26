using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameStateCondition : ScriptableObject
{
    private GameState linkedState;



    public abstract bool ConditionCheck(GameState lastState);


}
