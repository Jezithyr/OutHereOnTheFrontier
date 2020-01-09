using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Module: ScriptableObject
{
    [SerializeField]
    private GameManager gameManager;
    [SerializeField]
    public bool RunUpdate = false;
    
    public virtual void Initialize()
    {

    }

    

    public virtual void Update()
    {

    }
}
