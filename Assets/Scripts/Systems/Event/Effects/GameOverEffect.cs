using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EventSystem/Effects/GameOver Effect")]
public class GameOverEffect : EventEffect
{
    
    [SerializeField] private PlayingState playState;

    public override void Run(ScriptableObject callingObject)
    {
        playState.Win();
    }
}
