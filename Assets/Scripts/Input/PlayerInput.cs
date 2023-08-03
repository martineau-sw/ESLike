using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ESLike 
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
        
        GameObject _player;
        EntityMotor _motor;
        PlayerCamera _characterCamera;

        bool _runToggle;
        
        void Awake()
        {
            _player = GameObject.FindWithTag("Player");
            _motor = _player.GetComponent<EntityMotor>();
            _characterCamera = Camera.main.transform.root.GetComponent<PlayerCamera>();

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

        void FixedUpdate()
        {
            WirePlayerCamera();
        }

        void Update()
        {
            UpdateInputs();
            WirePlayerMovement();
        }

        void LateUpdate()
        {
            
        }

        void UpdateInputs() 
        {
            ToggleRun();

            _moveInput = _moveAction.ReadValue<Vector2>();
            _lookInput = _lookAction.ReadValue<Vector2>();
        }

        void WirePlayerMovement()
        {
            Vector2 moveInputRotated;
            float theta = _characterCamera.transform.eulerAngles.y * Mathf.Deg2Rad;

            moveInputRotated.x = Mathf.Cos(theta) * _moveInput.x + Mathf.Sin(theta) * _moveInput.y;
            moveInputRotated.y = Mathf.Cos(theta) * _moveInput.y - Mathf.Sin(theta) * _moveInput.x;

            Vector3 direction = new Vector3(moveInputRotated.x, 0, moveInputRotated.y);
            direction = Vector3.Lerp(direction, moveInputRotated, 5 * Time.deltaTime);
            direction.x = Mathf.Clamp(direction.x, -1, 1);
            direction.z = Mathf.Clamp(direction.z, -1, 1);

            _motor.Sprint = _sprintAction.IsPressed();

            direction *= _runToggle || _motor.Sprint ? 1f : 0.5f;

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
    }
}