using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ESLike.Utilities;


namespace ESLike.Actor
{
    public class ActorMotor : MonoBehaviour
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
        [Range(0, 75)]
        float _slopeLimit;
        [SerializeField]
        [Range(0, 0.5f)]
        float _stepLimit;
        [SerializeField]
        [Range(0, 5)]
        float _jumpHeight;


        [Header("Jump")]
        [SerializeField]
        [Range(0, 1)]
        float _groundedDistance;
        [SerializeField]
        LayerMask _groundLayer;

        #region Internal
        float _moveDelay;
        float _stepHeight;
        Rigidbody _rigidbody;
        CapsuleCollider _collider;
        Vector3 _lastDirection;
        #endregion
        
        #region Properties
        
        public float Speed => _rigidbody.velocity.magnitude;
        protected bool Sprint {get; set;}
        public bool Grounded  {get;set;}
        public bool Walk {get; set;}
        public bool HardLanding {get; set;}
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
        bool CanStepOver => _stepHeight < _stepLimit && Mathf.Abs(_stepHeight) > Mathf.Epsilon && Grounded;
            
        #endregion

        protected void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _collider = GetComponent<CapsuleCollider>();
        }

        protected void Update() 
        {
            Grounded = CheckGrounded();
            PreventMovementTimer();
        }

        protected void LateUpdate()
        {
            TriggerHardLanding();
        }

        protected void Move(Vector3 direction) 
        {
            direction = Vector3.Lerp(_lastDirection, direction, 6 * Time.deltaTime);
            Vector3 targetPosition = _rigidbody.position + direction * TargetMoveSpeed * Time.deltaTime;
            Vector3 targetVelocity = (targetPosition - transform.position) / Time.deltaTime;
            _lastDirection = direction;

            // Enforce slope limit
            if(!CheckSlope(targetVelocity))
            {
                direction.x = 0;
                direction.z = 0;
            }

            // Rotate towards movement direction
            if(targetVelocity != Vector3.zero)
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(targetVelocity), 20 * Time.deltaTime);

            _stepHeight = GetStepHeight(targetVelocity);

            targetVelocity.y = CanStepOver ?  _jumpHeight * _stepHeight : _rigidbody.velocity.y;
            
            _rigidbody.velocity = targetVelocity;
        } 

        public void Jump()
        {
            Vector3 velocity = _rigidbody.velocity;
            velocity.y = _jumpHeight;
            if(Grounded) _rigidbody.velocity = velocity;
        }

        void TriggerHardLanding() 
        {
            if(!Grounded && _rigidbody.velocity.y < -20) 
            {
                HardLanding = true;
                _moveDelay = 5.0f;
                return;
            }

            HardLanding = HardLanding && _moveDelay > 0f;
        }

        void PreventMovementTimer() 
        {
            _moveDelay = _moveDelay > 0 ? _moveDelay - Time.deltaTime : _moveDelay;
        }


        float GetStepHeight(Vector3 direction) 
        {
            bool rayHit = Physics.Raycast(new Ray(transform.position + Vector3.up, Vector3.down), out RaycastHit hit, 0.5f, _groundLayer);
            return rayHit ? 0.5f - hit.distance : 0f;
        }

        bool CheckSlope(Vector3 direction) 
        {
            bool lineHit = Physics.Linecast(transform.position, transform.position + direction.normalized * _collider.radius, out RaycastHit hit, _groundLayer);
            return (lineHit ? Vector3.Angle(Vector3.up, hit.normal) : 0f) < _slopeLimit;
        }
        
        bool CheckGrounded()
        {
            float distance = 10;
            Ray ray = new Ray(transform.position + Vector3.up, Vector3.down * distance);
            if (Physics.Raycast(ray, out RaycastHit hit, distance, _groundLayer))
                distance = hit.distance;

            Debug.DrawRay(ray.origin, ray.direction * distance, Color.red);
            return distance - 0.5f <= _groundedDistance;
        }
    }
}