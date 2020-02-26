using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ScriptedCamera : ScriptableObject
{
    [SerializeField] protected CameraModule cameraManager;

    [SerializeField] protected Camera cameraObj;
    public Camera CreatedCamera{get => cameraObj;}
    
    public abstract void CameraUpdate();

    public abstract void Initalize();

    public bool Active = false;
}
