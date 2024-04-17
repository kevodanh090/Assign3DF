using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    Animator animator;
    PlayerLocomotion playerLocomotion;
    CameraManager cameraManager;
    InputManager inputManager;

    public bool isInteracting;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
        inputManager = GetComponent<InputManager>();
        cameraManager = FindObjectOfType<CameraManager>();
    }
    private void Update()
    {
        inputManager.HandleAllInputs();
    }
    private void FixedUpdate()
    {
        playerLocomotion.HandleAllMovement();
    }
    private void LateUpdate()
    {
        
        cameraManager.HandleAllCameraMovement();

        isInteracting = animator.GetBool("isInteracting");

        playerLocomotion.isJumping = animator.GetBool("isJumping");

        animator.SetBool("isGrounded", playerLocomotion.isGrounded);

    }
}
