using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;
    private CharacterController player;
    private Vector3 moveDirection;

    [SerializeField] private float moveSpeed = 5f , rotationSpeed;
    [SerializeField] private float verticalVelocity, gravityForce, jumpForce;

    private void Awake()
    {
        instance = this;
        player = GetComponent<CharacterController>();
        gravityForce = 9.8f;
        jumpForce = 4f;
        rotationSpeed = 5f;
    }

    private void Start()
    {
        InputManager.Instance.OnJump += InputManager_OnJump;
    }


    private void InputManager_OnJump(object sender, System.EventArgs e)
    {
        if (player.isGrounded)
        {
            verticalVelocity = jumpForce;
        }
    }

    private void Update()
    {
        HandleMovement();
        ApplyGravity();
        player.Move(moveDirection * Time.deltaTime);
    }

    private void HandleMovement()
    {
        if (player.isGrounded)
        {
            if (verticalVelocity < 0) // if player is not jumping
            {
                verticalVelocity = -1f; // Apply downward gravity force
            }
        }
        // Get input direction
        Vector2 direction = InputManager.Instance.GetNormalizedVector2Movement();
        if (direction.y < 0) direction.y = 0; //prevents moving in any direction except its forward while jumping
        moveDirection = new Vector3(direction.x, 0f, direction.y);
        moveDirection = transform.TransformDirection(moveDirection) * moveSpeed;

        HandleRotation(moveDirection);
    }

    private void HandleRotation(Vector3 moveDirection)
    {
        if (moveDirection.sqrMagnitude > 0.01f) // Ensure there's movement
        {
            Quaternion targetDirection = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetDirection, rotationSpeed * Time.deltaTime);
        }
    }

    private void ApplyGravity()
    {
        if (!player.isGrounded)
        {
            verticalVelocity -= gravityForce * Time.deltaTime;
        }
        moveDirection.y = verticalVelocity;
    }

    public bool IsRunning()
    {
        return player.isGrounded && moveDirection.sqrMagnitude > 1f;
    }

    public bool IsJumping()
    {
        return !player.isGrounded;
    }
}
