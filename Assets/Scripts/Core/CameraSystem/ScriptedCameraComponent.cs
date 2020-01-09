using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptedCameraComponent : MonoBehaviour
{
    [SerializeField] private ScriptedCamera LinkedScriptObject;
    private bool hasValidScript = false;

    private void Start()
    {
        hasValidScript = LinkedScriptObject;
    }

    private void Update()
    {
        if (!hasValidScript) return;
        LinkedScriptObject.CameraUpdate();
    }
    
}
