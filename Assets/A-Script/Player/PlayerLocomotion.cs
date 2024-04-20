using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
    Transform cameraObject;
    public Rigidbody playerRigidbody;
    InputManager inputMangager;
    PlayerManager playerManager;
    AnimatorManager animatorManager;
    
    Vector3 moveDirection;

    [Header("Falling")]
    public float inAirTimer;
    public float leapingVelocity;
    public float fallingVelocity;
    public float rayCastHeightOffSet = 0.5f;
    public LayerMask groundLayer;

    [Header ("Movement Flags")]
    public bool isSprinting;
    public bool isGrounded;
    public bool isJumping;

    [Header("Movement Speeds")]
    public float walkingSpeed = 1.5f;
    public float sprintingSpeed = 7;
    public float runningSpeed = 5;
    public float rotaionSpeed = 15;

    [Header("Jump Speeds")]
    public float jumpHeight = 3;
    public float gravityIntensity = -15;


    private void Awake()
    {
        animatorManager = GetComponent<AnimatorManager>();
        playerManager = GetComponent<PlayerManager>();  
        inputMangager = GetComponent<InputManager>();
        playerRigidbody = GetComponent<Rigidbody>();
        cameraObject = Camera.main.transform;
    }
    public void HandleAllMovement()
    {
        HandleFallingAndLanding();
        if (playerManager.isInteracting)
        {
            return;
        }
        HandleMovement();
        HandleRotation();
    }
    private void HandleMovement()
    {
        if (isJumping)
        {
            return;
        }

        moveDirection = cameraObject.forward * inputMangager.verticalInput;
        moveDirection = moveDirection + cameraObject.right * inputMangager.horizontalInput;
        moveDirection.Normalize();
        moveDirection.y = 0;

        if (isSprinting)
        {
            moveDirection = moveDirection * sprintingSpeed;
        }
        else
        {
            if (inputMangager.moveAmount >= 0.5f)
            {
                moveDirection = moveDirection * runningSpeed;
            }
            else
            {
                moveDirection = moveDirection * walkingSpeed;
            }
        }
        Debug.Log(moveDirection);
        Vector3 movementVelocity = moveDirection;
        playerRigidbody.velocity = movementVelocity;
    }
    private void HandleRotation()
    {
        if (isJumping)
        {
            return;
        }
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
    private void HandleFallingAndLanding()
    {
        RaycastHit hit;
        Vector3 rayCastOrigin = transform.position;
        rayCastOrigin.y = rayCastOrigin.y + rayCastHeightOffSet;

        if (!isGrounded && !isJumping)
        {
            if (!playerManager.isInteracting)
            {
                animatorManager.TargetAnimation("Falling", true);
            }

            animatorManager.animator.SetBool("isUsingRootMotion", false);
            inAirTimer = inAirTimer + Time.deltaTime;
            playerRigidbody.AddForce(transform.forward * leapingVelocity);
            playerRigidbody.AddForce(-Vector3.up * fallingVelocity * inAirTimer);
        }
        if (Physics.SphereCast(rayCastOrigin, 0.2f, -Vector3.up, out hit, groundLayer))
        {
            if (!isGrounded && !playerManager.isInteracting)
            {
                animatorManager.TargetAnimation("Land", true);
            }
            inAirTimer = 0;
            isGrounded = true;
        }
        else
        {
            isGrounded = false; 
        }
    }
    public void HandleJumping()
    {
        if(isGrounded)
        {
            animatorManager.animator.SetBool("isJumping", true);
            animatorManager.TargetAnimation("Jump", false);

            float jumpingVelocity = Mathf.Sqrt(-2 * gravityIntensity * jumpHeight);
            Vector3 playerVeloccity = moveDirection;
            Debug.Log(moveDirection);
            Debug.Log("x:" + playerVeloccity.x + "Z: " + playerVeloccity.z);
            playerVeloccity.y = jumpingVelocity;
            playerRigidbody.velocity = playerVeloccity;
        }
    }
    public void HandleDodge()
    {
        if (playerManager.isInteracting)
        {
            return;
        }
        animatorManager.TargetAnimation("Dodge", true, true);
        //TOGGLE INVULNERABLE BOOL FOR NO HP DMAGE DURING ANIMATION
    }
}
