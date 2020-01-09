using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Module: ScriptableObject
{
    [SerializeField] private GameManager gameManager;

    public abstract void Initialize();
}
