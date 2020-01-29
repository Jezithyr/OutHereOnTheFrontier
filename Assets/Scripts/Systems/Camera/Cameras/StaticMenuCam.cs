using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptedCameras/Create Static Menu Camera")]
public class StaticMenuCam : ScriptedCamera
{
    // Hold the Serialized Fields here
    [SerializeField] private GameObject prefab;
    [SerializeField] private float CameraAngle = 30;
    [SerializeField] private float CameraDistance = 50;
    [SerializeField] public Vector3 targetPosition;

    // Camera Properties

    private Camera cameraComponent;
    private GameObject cameraGameObject;

    public override void Initalize()
    {
        cameraGameObject = GameObject.Instantiate(prefab);
        cameraComponent = cameraGameObject.GetComponentInChildren<Camera>();
        cameraObj = cameraComponent;
        Debug.Log("Creating MenuCamera" + cameraGameObject + "\n");
        cameraComponent.enabled = true;
        cameraObj = cameraComponent;

        cameraComponent.transform.localPosition = ComputeCameraPos(CameraAngle, CameraDistance);
    }
    public void SetCameraPos(Vector3 worldPos)
    {
        targetPosition = worldPos;
        cameraGameObject.transform.position = worldPos;
    }

    public override void CameraUpdate()
    {
       
    }

    private Vector3 ComputeCameraPos(float camAngle, float distance)
    {
        camAngle = -(camAngle - 90) * Mathf.PI / 180;
        return new Vector3(0, Mathf.Cos(camAngle) * distance, Mathf.Sin(camAngle) * distance);

    }
}
