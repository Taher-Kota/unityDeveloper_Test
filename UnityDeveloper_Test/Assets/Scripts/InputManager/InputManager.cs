using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;
    private GameInputs gameInputs;
    public event EventHandler OnJump;
    public event EventHandler<OnHologramEventArgs> OnHologramActivate;
    public class OnHologramEventArgs : EventArgs
    {
        public string keyName;
    }
    public event EventHandler OnHologramDeacitvate;
    public event EventHandler<OnHologramEventArgs> OnManipulateGravity;
    private bool isHoloActivate;
    private string currentKeyName;

    private void Awake()
    {
        Instance = this;
        gameInputs = new GameInputs();
        gameInputs.Enable();
        gameInputs.Player.Jump.performed += Jump_performed;
        gameInputs.Hologram.CreateHologram.performed += CreateHologram_performed;
        gameInputs.Hologram.CreateHologram.canceled += CreateHologram_canceled;
        gameInputs.Hologram.ManipulateGravity.performed += ManipulateGravity_performed;
    }

    private void OnDestroy()
    {
        gameInputs.Dispose();
    }

    private void ManipulateGravity_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (isHoloActivate)
        {
            OnManipulateGravity?.Invoke(this, new OnHologramEventArgs
            {
                keyName = currentKeyName
            });
        }
    }

    private void CreateHologram_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        isHoloActivate = true;
        currentKeyName = obj.control.name;
        OnHologramActivate?.Invoke(this, new OnHologramEventArgs
        {
            keyName = currentKeyName
        });
    }

    private void CreateHologram_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        isHoloActivate = false;
        currentKeyName = null;
        OnHologramDeacitvate?.Invoke(this, EventArgs.Empty);
    }

    private void Jump_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnJump?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetNormalizedVector2Movement()
    {
        return gameInputs.Player.Move.ReadValue<Vector2>();
    } 
}
