using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptedCameraComponent : MonoBehaviour
{
    [SerializeField] public ScriptedCamera LinkedScriptObject;
    private bool hasValidScript = false;

    private void Start()
    {
        hasValidScript = LinkedScriptObject;
    }

    private void Update()
    {
        if (!hasValidScript || !LinkedScriptObject.Active) return;
        LinkedScriptObject.CameraUpdate();
    }
    
}
