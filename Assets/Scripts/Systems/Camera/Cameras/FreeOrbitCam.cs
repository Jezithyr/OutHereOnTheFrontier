using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ScriptedCameras/Create Orbit Camera")]
public class FreeOrbitCam : ScriptedCamera
{
    [SerializeField] private GameObject prefab;

    [SerializeField] private float CameraAngle = 30;
    [SerializeField] private float CameraDistance = 50;
    [SerializeField] private float MaxPanX = 100;
    [SerializeField] private float MaxPanZ = 100;
    [SerializeField] private float CameraPanSpeed = 5;
    [SerializeField] private float CameraRotationSpeed = 1;

    [SerializeField] private float minZoom = 1;
    [SerializeField] private float maxZoom = 10;

    [SerializeField] public Vector3 targetPosition;

    [SerializeField] private string _moveXInput = "Horizontal";  // movement input string
    [SerializeField] private string _moveZInput = "Vertical";  // movement input string
    [SerializeField] private string _zoomAxis = "Zoom";  // movement input string
    [SerializeField] private string _rotateAxis = "Rotate";  // movement input string

    private Quaternion _cameraRotation;
    private float _targetRotation;

    

    private Camera cameraComponent;

    private GameObject cameraGameObject;

   
    public override void Initalize()
    {
        cameraGameObject = GameObject.Instantiate(prefab);
        cameraComponent = cameraGameObject.GetComponentInChildren<Camera>();
        cameraObj = cameraComponent;
        Debug.Log("Creating OrbitCam"+cameraGameObject + "\n");
        cameraComponent.enabled = true;
        cameraObj = cameraComponent;
    }

    private Quaternion ComputeCameraRotation(float camAngle)
    {
        // camAngle; //adjust the angle from the input value
        float newAngle = camAngle;

        return Quaternion.Euler(newAngle, 180, 0);
    }

    private Vector3 ComputeCameraPos(float camAngle, float distance)
    {

        camAngle = -(camAngle - 90) * Mathf.PI / 180;
        return new Vector3(0, Mathf.Cos(camAngle) * distance, Mathf.Sin(camAngle) * distance);

    }

    public void SetCameraPos(Vector3 worldPos)
    {
        targetPosition = worldPos;
        cameraGameObject.transform.position = worldPos;
    }

    private void PanCamera(float XPanInput, float ZPanInput)
    {
        



        cameraGameObject.transform.position = (cameraGameObject.transform.forward * -XPanInput *CameraPanSpeed) + (cameraGameObject.transform.right * ZPanInput *CameraPanSpeed) + cameraGameObject.transform.position;

        targetPosition = cameraGameObject.transform.position;
    }

    private void RotateCamera(float roationInput)
    {
        float oldRotation = cameraGameObject.transform.rotation.eulerAngles.y;
        _targetRotation = oldRotation + (roationInput * CameraRotationSpeed);
        cameraGameObject.transform.rotation = Quaternion.Euler(0, _targetRotation, 0);


    }

    public override void CameraUpdate()
    {
        float moveZInput = Input.GetAxis(_moveZInput);
        float moveXInput = -Input.GetAxis(_moveXInput);
        float rotateInput = Input.GetAxis(_rotateAxis);
        
        PanCamera(moveZInput, moveXInput);
        RotateCamera(rotateInput);
        cameraComponent.transform.localPosition = ComputeCameraPos(CameraAngle, CameraDistance);
    
        cameraComponent.transform.localRotation = ComputeCameraRotation(CameraAngle);
    }
}
