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
        InputAction _runAction;
        InputAction _sprintAction;
        InputAction _jumpAction;
        InputAction _lookAction;
        
        Vector2 _moveInput;
        Vector2 _lookInput;
        
        #region MonoBehaviours
        GameObject _player;
        ActorMono _actor;
        PlayerCamera _characterCamera;
        #endregion

        bool _runToggle;
        
        void Start()
        {
            _runToggle = true;
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
            _actor = GameObject.FindWithTag("Player").GetComponent<ActorMono>();
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
            _actor.Walk = _runToggle;
            _actor.SprintInput = _sprintAction.IsPressed();
            _actor.DirectionInput = AdjustMoveDirectionToCamera(_moveInput);
            if(_jumpAction.IsPressed()) _actor.Jump();
        }

        void WirePlayerCamera()
        {
            _characterCamera.UpdateCamera(_lookInput);
        }

        Vector3 AdjustMoveDirectionToCamera(Vector2 input)
        {
            input = input.Rotate(_characterCamera.transform.eulerAngles.y);
            Vector3 direction = input.Vector2ToXZ().ClampXZ(-1, 1);
            return direction.magnitude > 1f ? direction.normalized : direction;
        }

        void ToggleRun()
        {
            if(_runAction.WasPressedThisFrame()) _runToggle = !_runToggle;
        }
    }
}