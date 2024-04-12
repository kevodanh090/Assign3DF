using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
    Transform cameraObject;
    Rigidbody playerRigidbody;
    InputManager inputMangager;
    
    Vector3 moveDirection;

    public float moveSpeed = 10;
    public float rotaionSpeed = 15;


    private void Awake()
    {
        inputMangager = GetComponent<InputManager>();
        playerRigidbody = GetComponent<Rigidbody>();
        cameraObject = Camera.main.transform;
    }
    public void HandleAllMovement()
    {
        HandleMovement();
        HandleRotation();
    }
    private void HandleMovement()
    {
        moveDirection = cameraObject.forward * inputMangager.verticalInput;
        moveDirection = moveDirection + cameraObject.right * inputMangager.horizontalInput;
        moveDirection.Normalize();
        moveDirection.y = 0;
        moveDirection = moveDirection * moveSpeed;

        Vector3 movementVelocity = moveDirection;
        playerRigidbody.velocity = movementVelocity;
    }
    private void HandleRotation()
    {
        Vector3 targetDirection = Vector3.zero;

        targetDirection = cameraObject.forward * inputMangager.verticalInput;
        targetDirection = targetDirection + cameraObject.right * inputMangager.horizontalInput;
        targetDirection.Normalize();
        targetDirection.y = 0;
        if(targetDirection == Vector3.zero)
        {
            targetDirection = transform.forward;
        }

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        Quaternion playerRotation = Quaternion.Lerp(transform.rotation, targetRotation, rotaionSpeed * Time.deltaTime);

        transform.rotation = playerRotation;

    }
}
