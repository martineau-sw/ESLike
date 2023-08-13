using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using ESLike.Utilities;
using ESLike.Actor;
using System;

namespace ESLike.Player
{
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField]
        InputActionAsset _actionAsset;

        InputAction _moveAction;
        InputAction _runAction;
        InputAction _sprintAction;
        InputAction _jumpAction;
        InputAction _lookAction;
        
        Vector2 _moveInput;
        Vector2 _lookInput;
        
        #region MonoBehaviours
        GameObject _player;
        PlayerMotor _motor;
        PlayerCamera _camera;
        #endregion

        bool _runToggle;
        
        void Start()
        {
            _runToggle = true;
            GetComponents();
            ExpressionEvaluator.Evaluate("2+2", out int result);
            Debug.Log(result);
        }

        void Awake()
        {
            InitializeInput();
        }

        void FixedUpdate()
        {
            WirePlayerCamera();
        }

        void Update()
        {
            UpdateInputs();
            WirePlayerMovement();
        }

        void GetComponents()
        {
            _motor = GameObject.FindWithTag("Player").GetComponent<PlayerMotor>();
            _camera = Camera.main.transform.root.GetComponent<PlayerCamera>();
        }

        void InitializeInput() 
        {
            _moveAction = _actionAsset.FindActionMap("navigation").FindAction("move");
            _runAction = _actionAsset.FindActionMap("navigation").FindAction("run");
            _sprintAction = _actionAsset.FindActionMap("navigation").FindAction("sprint");
            _jumpAction = _actionAsset.FindActionMap("navigation").FindAction("jump");
            _lookAction = _actionAsset.FindActionMap("navigation").FindAction("look");

            _moveAction.Enable();
            _runAction.Enable();
            _jumpAction.Enable();
            _sprintAction.Enable();
            _lookAction.Enable();
        }

        void UpdateInputs() 
        {
            ToggleRun();

            _moveInput = _moveAction.ReadValue<Vector2>();
            _lookInput = _lookAction.ReadValue<Vector2>();
        }

        void WirePlayerMovement()
        {
            _motor.Walk = _runToggle;
            _motor.Sprint = _sprintAction.IsPressed();
            _motor.TargetVelocity = AdjustMoveDirectionToCamera(_moveInput);
            if(_jumpAction.IsPressed()) _motor.Jump();
        }

        void WirePlayerCamera()
        {
            _camera.UpdateCamera(_lookInput);
        }

        Vector3 AdjustMoveDirectionToCamera(Vector2 input)
        {
            input = input.Rotate(_camera.transform.eulerAngles.y);
            Vector3 direction = input.Vector2ToXZ().ClampXZ(-1, 1);
            return direction.magnitude > 1f ? direction.normalized : direction;
        }

        void ToggleRun()
        {
            if(_runAction.WasPressedThisFrame()) _runToggle = !_runToggle;
        }
    }
}