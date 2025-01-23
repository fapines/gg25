using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityStandardAssets._2D;

// This class reads the input from player and feeds it to the corresponding CharacterController
public class Player : MonoBehaviour
{
    // Public fields
    public PlatformerCharacter2D characterController;

    // Player input by input system
    [SerializeField] private InputAction movement;
    [SerializeField] private InputAction jump;
    [SerializeField] private InputAction shoot;
    [SerializeField] private InputAction aim;
    [SerializeField] private InputAction shootGrenade;

    // Usable player input data
    private Vector2 direction { get; set; }
    public Vector2 aimDirection { get; set; }
    private bool jumping = false;
    private bool shooting = false;
    private bool shootingGrenade = false;

    private void Awake()
    {
        movement.performed += OnMovementPerformed;
        movement.canceled += OnMovementPerformed;
        jump.performed += OnJumpPerformed;
        jump.canceled += OnJumpPerformed;
        shoot.performed += OnShootPerformed;
        shoot.canceled += OnShootPerformed;
        shootGrenade.performed += OnShootGrenadePerformed;
        shootGrenade.canceled += OnShootGrenadePerformed;
        aim.performed += OnAimPerformed;
        //aim.canceled += OnAimPerformed;


    }

    private void OnEnable()
    {
        movement.Enable();
        jump.Enable();
        shoot.Enable();
        aim.Enable();
        shootGrenade.Enable();
    }

    private void OnDisable()
    {
        movement.Disable();
        jump.Disable();
        shoot.Disable();
        aim.Disable();
        shootGrenade.Disable();
    }

    void FixedUpdate()
    {
        characterController.Move(direction.x, false, jumping);
        characterController.shoot(shooting);
        characterController.shootGrenade(shootingGrenade);
        characterController.aimingDirection = aimDirection;
    }

    private void OnMovementPerformed(InputAction.CallbackContext ctx)
    {
        direction = ctx.ReadValue<Vector2>();

    }

    private void OnJumpPerformed(InputAction.CallbackContext ctx)
    {
        jumping = ctx.performed;


    }

    private void OnAimPerformed(InputAction.CallbackContext ctx)
    {
        aimDirection = ctx.ReadValue<Vector2>();


    }

    private void OnShootPerformed(InputAction.CallbackContext ctx)
    {
        shooting = ctx.performed;
    }
    private void OnShootGrenadePerformed(InputAction.CallbackContext ctx)
    {
        shootingGrenade = ctx.performed;
    }
}