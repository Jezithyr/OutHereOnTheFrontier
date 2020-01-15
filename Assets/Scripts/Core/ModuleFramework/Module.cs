using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Module: ScriptableObject
{
    [SerializeField]
    public ModuleManager moduleManager;

    [SerializeField]
    public bool RunUpdate = false;

    public virtual void Initialize()
    {

    }

    public virtual void Update()
    {

    }
}
