using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameState : ScriptableObject
{
    [SerializeField] public bool CanTick;
    [SerializeField] public long tickRate;

    public abstract void OnActivate(GameState lastState);

    public abstract void OnDeactivate(GameState newState);

    public virtual void OnUpdate(){}

}
