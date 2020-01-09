using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Module: ScriptableObject
{
    [SerializeField]
    private GameManager gameManager;
    [SerializeField]
    public bool RunUpdate = false;
    [SerializeField]
    public long TickTime = 1;
    public virtual void Initialize()
    {

    }

    

    public virtual void Update()
    {

    }
}
