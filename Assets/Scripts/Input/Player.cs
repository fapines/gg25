using UnityEngine;
using UnityEngine.InputSystem;
using UnityStandardAssets._2D;

public class Player : MonoBehaviour
{
    public PlatformerCharacter2D characterController;

    // Player input by input system
    [SerializeField] private InputAction movement;
    [SerializeField] private InputAction jump;
    [SerializeField] private InputAction shoot;
    [SerializeField] private InputAction aim;

    // Usable player input data
    private Vector2 direction { get; set; }
    private Vector2 aimDirection { get; set; }
    private bool jumping = false;
    private bool shooting = false;

    private Camera mainCamera; // Reference to the main camera for getting mouse position

    private void Awake()
    {
        movement.performed += OnMovementPerformed;
        movement.canceled += OnMovementPerformed;
        jump.performed += OnJumpPerformed;
        jump.canceled += OnJumpPerformed;
        shoot.performed += OnShootPerformed;
        shoot.canceled += OnShootPerformed;
        aim.performed += OnAimPerformed;

        mainCamera = Camera.main; // Assign the main camera
    }

    private void OnEnable()
    {
        movement.Enable();
        jump.Enable();
        shoot.Enable();
        aim.Enable();
    }

    private void OnDisable()
    {
        movement.Disable();
        jump.Disable();
        shoot.Disable();
        aim.Disable();
    }

    void FixedUpdate()
    {
        characterController.Move(direction.x, false, jumping); // Move the character based on input
        characterController.shoot(shooting); // Handle shooting
        characterController.aimingDirection = aimDirection; // Set the aiming direction
    }

    private void OnMovementPerformed(InputAction.CallbackContext ctx)
    {
        direction = ctx.ReadValue<Vector2>(); // Get horizontal and vertical movement
    }

    private void OnJumpPerformed(InputAction.CallbackContext ctx)
    {
        jumping = ctx.performed; // Jumping state
    }

    private void OnAimPerformed(InputAction.CallbackContext ctx)
    {
        // Calculate the aiming direction based on mouse position
        Vector2 mousePos = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        aimDirection = mousePos - (Vector2)transform.position; // Direction from player to mouse
        aimDirection.Normalize(); // Normalize the direction to avoid scaling the aim
    }

    private void OnShootPerformed(InputAction.CallbackContext ctx)
    {
        shooting = ctx.performed; // Shooting state
    }
}
