using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UserInput : MonoBehaviour
{
    public static UserInput Instance;
    

    #region Input mappings

    public Vector2 MoveInput { get; private set; }

    public bool InteractButtonPressedThisFrame { get; private set; }
    public bool InteractButtonHeldThisFrame { get; private set; }
    public bool PauseMenuPressedThisFrame { get; private set; }
    
    
    private PlayerInput _playerInput;

    private InputAction _moveAction;
    private InputAction _menuAction;
    private InputAction _interactAction;
    #endregion



    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("More than one UserInput instance in scene!");
            return;
        }
        Instance = this;

        _playerInput = GetComponent<PlayerInput>();
        
        _moveAction = _playerInput.actions["Move"];
        _interactAction = _playerInput.actions["Interact"];
        _menuAction = _playerInput.actions["PauseMenu"];
    }

    private void Update()
    {
        InteractButtonPressedThisFrame = _interactAction.WasPressedThisFrame();
        InteractButtonHeldThisFrame = _interactAction.IsPressed();

        PauseMenuPressedThisFrame = _menuAction.WasPressedThisFrame();
        
        MoveInput = _moveAction.ReadValue<Vector2>();
    }
}
