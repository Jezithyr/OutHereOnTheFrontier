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

    [SerializeField] private LayerMask cameraOcclusionLayer;


    private CameraCollisionPosLinks collisionPosLinks;

    private Vector3 checkPosFwd = new Vector3();
    private Vector3 checkPosBwk = new Vector3();
    private Vector3 checkPosRight = new Vector3();
    private Vector3 checkPosLeft = new Vector3();

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
        collisionPosLinks = cameraGameObject.GetComponentInChildren<CameraCollisionPosLinks>();
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

        bool zBoundsCheck1 = ((Physics.OverlapSphere(collisionPosLinks.checkPosFwdPrefab.transform.position,2,cameraOcclusionLayer).Length == 0));
        bool zBoundsCheck2 =  ((Physics.OverlapSphere(collisionPosLinks.checkPosBwkPrefab.transform.position,2,cameraOcclusionLayer).Length == 0));
        bool xBoundsCheck1 =  ((Physics.OverlapSphere(collisionPosLinks.checkPosLeftPrefab.transform.position,2,cameraOcclusionLayer).Length == 0));
        bool xBoundsCheck2 = ((Physics.OverlapSphere(collisionPosLinks.checkPosRightPrefab.transform.position,2,cameraOcclusionLayer).Length == 0));
        
        if (!xBoundsCheck1)
        {
            moveXInput = Mathf.Clamp(moveXInput,0,1);
        }
        if (!xBoundsCheck2)
        {
            moveXInput = Mathf.Clamp(moveXInput,-1,0);
        }
        if (!zBoundsCheck1)
        {
            moveZInput = Mathf.Clamp(moveZInput,0,1);
        }
        if (!zBoundsCheck2)
        {
            moveZInput = Mathf.Clamp(moveZInput,-1,0);
        }
        if(Time.deltaTime <= 0.166666)//
        {
            PanCamera(moveZInput, moveXInput);
        RotateCamera(rotateInput);
        }
        
        cameraComponent.transform.localPosition = ComputeCameraPos(CameraAngle, CameraDistance);
    
        cameraComponent.transform.localRotation = ComputeCameraRotation(CameraAngle);
    }
}
