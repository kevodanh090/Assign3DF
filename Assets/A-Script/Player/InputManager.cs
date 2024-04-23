using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    PlayerControls playerControls;
    PlayerLocomotion playerLocomotion;
    AnimatorManager animatorManager;
    PlayerAttacker playerAttacker;
    PlayerInventory playerInventory;

    public Vector2 movementInput;
    public Vector2 cameraInput;

    public float cameraInputX;
    public float cameraInputY;

    public float moveAmount;
    public float verticalInput;
    public float horizontalInput;

    public bool sprintInput;
    public bool jumpInput;
    public bool dodgeImput;
    public bool lAttackInput;
    public bool hAttackInput;
    private void Awake()
    {
        animatorManager = GetComponent<AnimatorManager>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
        playerAttacker = GetComponent<PlayerAttacker>();
        playerInventory = GetComponent<PlayerInventory>();
    }
    private void OnEnable()
    {
        if(playerControls == null)
        {
            playerControls = new PlayerControls();
            playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
            playerControls.PlayerMovement.CameraRotate.performed += i => cameraInput = i.ReadValue<Vector2>();

            playerControls.PlayerActions.Sprint.performed += i => sprintInput = true;
            playerControls.PlayerActions.Sprint.canceled += i => sprintInput = false;

            playerControls.PlayerActions.Jump.performed += i => jumpInput = true;
            playerControls.PlayerActions.Jump.canceled += i => jumpInput = false;

            playerControls.PlayerActions.Dodge.performed += i => dodgeImput = true;
            playerControls.PlayerActions.Dodge.canceled += i => dodgeImput = false;
            
        }
        playerControls.Enable();
    }
    private void OnDisable()
    {
        playerControls.Disable();
    }
    public void HandleAllInputs()
    {
        HandleMovementInput();
        HandleSprintInput();
        HandleJumpInput();
        HandleDodgeInput();
        HandleAttackInput();
    }
    private void HandleMovementInput()
    {
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;

        cameraInputY = cameraInput.y;
        cameraInputX = cameraInput.x;
      
        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
        animatorManager.UpdateAnimatorValues(0, moveAmount, playerLocomotion.isSprinting);
    }
    private void HandleSprintInput()
    {
        if (sprintInput && moveAmount > 0.5f)
        {
            playerLocomotion.isSprinting = true;
        }
        else
        {
            playerLocomotion.isSprinting = false;
        }
    }
    private void HandleJumpInput()
    {
        if (jumpInput)
        {
            jumpInput = false;
            playerLocomotion.HandleJumping();
        }
    }
    private void HandleDodgeInput()
    {
        if (dodgeImput)
        {
            dodgeImput = false;
            playerLocomotion.HandleDodge();
        }
    }
    private void HandleAttackInput()
    {
        playerControls.PlayerActions.LAttack.performed += i => lAttackInput = true;
        playerControls.PlayerActions.LAttack.canceled += i => lAttackInput = false;
        playerControls.PlayerActions.HAttack.performed += i => hAttackInput = true;
        playerControls.PlayerActions.HAttack.canceled += i => hAttackInput = false;

        if (lAttackInput)
        {
            playerAttacker.HandleLightAttack(playerInventory.rightWeapon);
        }
        if (hAttackInput)
        {
            playerAttacker.HandleHeavyAttack(playerInventory.rightWeapon);
        }
    }
}
