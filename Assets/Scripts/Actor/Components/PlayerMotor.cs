using UnityEngine;

namespace ESLike.Player 
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMotor : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField]
        [Range(1, 10)]
        float _baseSpeed = 1;
        [SerializeField]
        [Range(0, 1)]
        float _walkMultiplier;
        [SerializeField]
        [Range(1, 2)]
        float _sprintMultiplier;
        [SerializeField]
        [Range(0, 5)]
        float _jumpHeight;
        [SerializeField]
        float _gravity;



        Vector3 _velocity;
        float _moveDelay;
        CharacterController _controller;

        public bool Sprint {get; set;}
        public bool Walk {get; set;}
        public Vector3 TargetVelocity {get; set;}

        float MoveMultiplier 
        {
            get 
            {
                if(PreventMovement) return 0f;
                if(Sprint) return _sprintMultiplier;
                if(Walk) return _walkMultiplier;
                return 1f;
            }
        }
        bool PreventMovement => _moveDelay > 0f;
        float TargetMoveSpeed => _baseSpeed * MoveMultiplier;

        void Start() 
        {
            _controller = GetComponent<CharacterController>();
        }

        void Update() 
        {
            _velocity = _controller.isGrounded ? _velocity : ApplyGravity(TargetVelocity);
            
            _controller.Move((_velocity + TargetVelocity) * Time.deltaTime);
        }

        Vector3 ApplyGravity(Vector3 vector) 
        {
            vector.y -= _gravity;
            return vector;
        }

        public void Jump()
        {
            _velocity.y = _jumpHeight;
        }
    }
}