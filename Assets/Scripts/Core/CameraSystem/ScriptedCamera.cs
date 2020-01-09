﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ScriptedCamera : ScriptableObject
{
    [SerializeField] protected CameraManager cameraManager;
    
    public abstract void CameraUpdate();

    public abstract void Initalize();

    public bool Active = false;
}