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
        float _runSpeed = 1;
        [SerializeField]
        [Range(1, 10)]
        float _sprintSpeed;
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

        [Header("State")]
        [SerializeField]
        bool _isSprinting;
        [SerializeField]
        bool _isGrounded;

        [Header("Debug")]
        public float groundDistance;
        public float slopeAngle;
        public float stepHeight;
        public float footY;

        #region Internal
        float _moveSpeed;
        float _moveDelay;

        Rigidbody _rigidbody;
        CapsuleCollider _collider;
        #endregion
        
        #region Properties

        public float Speed => _rigidbody.velocity.magnitude;
        public bool Sprint {get; set;}
        public bool CanSprint {get; set;}
        public bool Airborne => !_isGrounded;
        public bool HardLanding {get; private set;}
        #endregion
        void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _collider = GetComponent<CapsuleCollider>();
        }

        void LateUpdate()
        {
            ReportState();
        }

        void ReportState()
        {
            _isGrounded = CheckGrounded();
            GetLanding();
        } 
        
        Vector3 _direction;

        public void Move(Vector3 direction) 
        {
            direction = Vector3.Lerp(_direction, direction, 6 * Time.deltaTime);

            Vector3 targetPosition = _rigidbody.position + direction * Throttle() * Time.deltaTime;

            _direction = direction;

            Vector3 targetVelocity = (targetPosition - transform.position) / Time.deltaTime;

            if(!CheckSlope(targetVelocity))
            {
                direction.x = 0;
                direction.z = 0;
            }
            
            if(targetVelocity != Vector3.zero)
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(targetVelocity), 20 * Time.deltaTime);

            targetVelocity.y = _rigidbody.velocity.y;
        
            stepHeight = GetStepHeight(targetVelocity);

            if(stepHeight < _stepLimit && Mathf.Abs(stepHeight) > Mathf.Epsilon && _isGrounded)
            {
                targetVelocity.y = _jumpHeight * stepHeight;
            }

            _rigidbody.velocity = targetVelocity;
        } 

        public void Jump(bool input)
        {
            Vector3 velocity = _rigidbody.velocity;
            velocity.y = _jumpHeight;
            if(input && CheckGrounded()) _rigidbody.velocity = velocity;
        }

        void GetLanding() 
        {
            if(!_isGrounded && _rigidbody.velocity.y < -20) 
            {
                HardLanding = true;
                _moveDelay = 5.0f;
                return;
            }

            HardLanding = HardLanding && _moveDelay > 0f;
        }

        float Throttle()
        {
            if(_moveDelay > 0f)
            {
                _moveDelay -= Time.deltaTime;
                return 0f;
            } 

            if(CanSprint && Sprint) return _sprintSpeed;
            return _runSpeed;
        }

        float GetStepHeight(Vector3 direction) 
        {
            Vector3 origin = transform.position + Vector3.up;
            origin += direction.normalized;
            Ray ray = new Ray(origin, Vector3.down);
            float height = 0;

            if (Physics.Raycast(ray, out RaycastHit hit, 0.5f, _groundLayer))
            {
                height = 0.5f - hit.distance;
            }

            Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.green);

            return height;
        }

        bool CheckSlope(Vector3 direction) 
        {
            float angle = 0;

            if (Physics.Linecast(transform.position, transform.position + direction.normalized * _collider.radius, out RaycastHit hit, _groundLayer))
            {   
                angle = Vector3.Angle(Vector3.up, hit.normal);
            }

            return angle < _slopeLimit;
        }

        bool CheckGrounded() 
        {
            float distance = 10;
            Ray ray = new Ray(transform.position + Vector3.up, Vector3.down * distance);
            if (Physics.Raycast(ray, out RaycastHit hit, distance, _groundLayer))
                distance = hit.distance;

            groundDistance = distance - 0.5f;

            Debug.DrawRay(ray.origin, ray.direction * distance, Color.red);
            return distance - 0.5f <= _groundedDistance;
        }
    }
}