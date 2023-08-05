using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using ESLike.Utilities;
using ESLike.Actor;

namespace ESLike.Player
{
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField]
        InputActionAsset _actionAsset;

        InputAction _moveAction;
        InputAction _runActions;
        InputAction _sprintAction;
        InputAction _jumpAction;
        InputAction _lookAction;
        
        Vector2 _moveInput;
        Vector2 _lookInput;
        
        #region MonoBehaviours
        GameObject _player;
        ActorMotor _motor;
        PlayerCamera _characterCamera;
        #endregion

        bool _runToggle;

        float SpeedMultiplier => _runToggle || _motor.Sprint ? 1f : 0.5f;
        
        void Start()
        {
            GetComponents();
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
            _player = GameObject.FindWithTag("Player");
            _motor = _player.GetComponent<ActorMotor>();
            _characterCamera = Camera.main.transform.root.GetComponent<PlayerCamera>();
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
            Vector2 moveInputRotated = _moveInput.Rotate(_characterCamera.transform.eulerAngles.y);
            Vector3 direction = ClampXZ(direction, moveInputRotated.Vector2ToXZ(), 5f);

            _motor.Walk = _runToggle;
            _motor.Sprint = _sprintAction.IsPressed();

            direction *= SpeedMultiplier;

            if(direction.magnitude > 1f) direction.Normalize();
            
            _motor.Move(direction);
            _motor.Jump(_jumpAction.WasPressedThisFrame());
        }

        void WirePlayerCamera()
        {
            _characterCamera.UpdateCamera(_lookInput);
        }

        void ToggleRun()
        {
            if(_runAction.WasPressedThisFrame()) _runToggle = !_runToggle;
        }

        Vector3 ClampXZ(Vector3 raw, Vector3 desired, float stepCoeffecient) 
        {
            direction.x = Mathf.Clamp(direction.x, -1, 1);
            direction.z = Mathf.Clamp(direction.z, -1, 1);

            return direction;
        }
    }
}