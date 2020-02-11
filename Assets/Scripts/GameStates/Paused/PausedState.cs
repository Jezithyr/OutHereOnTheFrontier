using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameFramework/Gamestate/Paused")]
public class PausedState : GameState
{
    
    [SerializeField]
    GameManager Game;



    public override void OnActivate(GameState lastState)
    {
        Game.Pause();

    }

    public override void OnUpdate()
    {
        
    }

    public override void OnDeactivate(GameState newState)
    {

    }

    public override void Reset()
    {
        
    }
}
