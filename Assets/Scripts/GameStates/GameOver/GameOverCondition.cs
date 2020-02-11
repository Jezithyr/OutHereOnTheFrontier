using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverCondition : GameStateCondition
{
    [SerializeField]
    GameState playState;

    [SerializeField] private ResourceModule resourceManager;

    [SerializeField] private Resource popResource;


    public override bool ConditionCheck(GameState lastState)
    {
        return (((PlayingState)playState).GameTimer <= 0 || resourceManager.GetResourceStorage(popResource) <= 0);
    }
}
