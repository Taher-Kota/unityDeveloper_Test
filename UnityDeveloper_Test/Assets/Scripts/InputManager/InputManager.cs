using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;
    private GameInputs gameInputs;
    public event EventHandler OnJump;
    private void Awake()
    {
        Instance = this;
        gameInputs = new GameInputs();
        gameInputs.Enable();
        gameInputs.Player.Jump.performed += Jump_performed;
    }

    private void Jump_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnJump?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetNormalizedVector2Movement()
    {
        Vector2 MoveDirection = gameInputs.Player.Move.ReadValue<Vector2>();
        return MoveDirection;
    } 
}
