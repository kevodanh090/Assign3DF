using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Transform targetTransform;     // The obbject the camera will follow
    [SerializeField] private Transform cameraPivot;         // The obbject the camera uses to pivot (look up and down)
    [SerializeField] private Transform cameraTransform;     // The transform of the actual camera object in the scene
    public LayerMask collisionLayers;                       //The layers I want my camera to collide with

    private float defaultPosition;
    private GameObject player;
    private InputManager inputManager;
    private Vector3 cameraFollowVelocity = Vector3.zero;
    private Vector3 cameraVectorPosition;

    public float cameraCollisionOffSet = 0.2f; //How much the camera will jump off of objects its colliding with
    public float minCollisionOffSet = 0.2f;
    public float cameraCollisionRadius = 2;
    public float cameraFollowSpeed = 0.2f;
    public float cameraLookSpeed = 2;
    public float cameraPivotSpeed = 2;

    public float lookAngle; //Camera looking up and down
    public float pivotAngle;    //Camera looking left and right
    public float minPivotAngle = -35;
    public float maxPivotAngle = 35;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        targetTransform = player.transform;
        inputManager = FindObjectOfType<InputManager>();
        cameraTransform = Camera.main.transform;
        defaultPosition = cameraTransform.localPosition.z;
    }

    public void HandleAllCameraMovement()
    {
        FollowTarget();
        RotateCamera();
        HandleCameraCollisions();
    }
    private void FollowTarget()
    {
        Vector3 targetPosition = Vector3.SmoothDamp
            (transform.position, targetTransform.position, ref cameraFollowVelocity, cameraFollowSpeed);

        transform.position = targetPosition;
    }
    private void RotateCamera()
    {
        Vector3 rotation;
        Quaternion targetRotation;

        lookAngle = lookAngle + (inputManager.cameraInputX * cameraLookSpeed);
        pivotAngle = pivotAngle - (inputManager.cameraInputY * cameraPivotSpeed);
        pivotAngle = Mathf.Clamp(pivotAngle, minPivotAngle, maxPivotAngle);

        rotation = Vector3.zero;
        rotation.y = lookAngle;
        targetRotation = Quaternion.Euler(rotation);
        transform.rotation = targetRotation;

        rotation = Vector3.zero;
        rotation.x = pivotAngle;
        targetRotation = Quaternion.Euler(rotation);
        cameraPivot.localRotation = targetRotation;

    }
    private void HandleCameraCollisions() 
    {
        float targetPosition = defaultPosition;
        RaycastHit hit;
        Vector3 direction = cameraTransform.position - cameraPivot.position;
        direction.Normalize();
        if (Physics.SphereCast
            (cameraPivot.transform.position, cameraCollisionRadius, direction, out hit, Mathf.Abs(targetPosition), collisionLayers)) 
        {
            float distance = Vector3.Distance(cameraPivot.position, hit.point);
            targetPosition = - (distance - cameraCollisionOffSet);
        } 
        if (Mathf.Abs(targetPosition) < minCollisionOffSet) 
        {
            targetPosition = targetPosition - minCollisionOffSet;
        }
        cameraVectorPosition.z = Mathf.Lerp(cameraTransform.localPosition.z, targetPosition, 0.2f);
        cameraTransform.localPosition = cameraVectorPosition;
    }
}
