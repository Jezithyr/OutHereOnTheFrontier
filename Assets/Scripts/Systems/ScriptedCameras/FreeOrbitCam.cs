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
        cameraGameObject.GetComponentInChildren<ScriptedCameraComponent>().LinkedScriptObject = this;
    }

    private Quaternion ComputeCameraRotation (float camAngle)
    {
       // camAngle; //adjust the angle from the input value
        float newAngle = camAngle;

        return  Quaternion.Euler(newAngle,180,0);
    }

    private Vector3 ComputeCameraPos(float camAngle,float distance)
    {

        camAngle = -(camAngle-90)*Mathf.PI/180;
        return new Vector3(0,Mathf.Cos(camAngle)*distance,Mathf.Sin(camAngle)*distance);

    }

    private void PanCamera(float XPanInput,float ZPanInput)
    {
        float oldX = cameraGameObject.transform.position.x;
        float oldZ = cameraGameObject.transform.position.z;
        cameraGameObject.transform.position = new Vector3((XPanInput*CameraPanSpeed)+oldX,cameraGameObject.transform.position.y,(ZPanInput*CameraPanSpeed)+oldZ);
    }

    private void RotateCamera(float roationInput)
    {
        float oldRotation = cameraGameObject.transform.rotation.eulerAngles.y;
        _targetRotation = oldRotation+(roationInput*CameraRotationSpeed);
        cameraGameObject.transform.rotation = Quaternion.Euler(0,_targetRotation,0);


    }

    public override void CameraUpdate()
    {
        PanCamera(Input.GetAxis(_moveZInput),-Input.GetAxis(_moveXInput));
        RotateCamera(Input.GetAxis(_rotateAxis));

        cameraComponent.transform.localPosition = ComputeCameraPos(CameraAngle,CameraDistance);
        cameraComponent.transform.localRotation = ComputeCameraRotation(CameraAngle);
    }
}
