using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameState : ScriptableObject
{
    public GameManager Manager;

    [SerializeField] public bool CanTick;
    [SerializeField] public long tickRate;

    public abstract void OnActivate(GameState lastState);

    public abstract void OnDeactivate(GameState newState);

    public abstract void Reset();

    public virtual void OnUpdate(){}

    public virtual void Initalize(){}

}
