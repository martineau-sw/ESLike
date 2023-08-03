using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ESLike 
{
    public class PlayerCamera : MonoBehaviour
    {

        [Header("Camera")]
        [SerializeField]
        Vector3 _cameraOffset;
        [SerializeField]
        float _desiredDistance;

        [Header("Debug")]
        Vector3 _euler;

        #region Internal
        GameObject _player;
        GameObject _target;
        Vector3 _position;
        float _distance;
        #endregion
        void Awake() 
        {
            _player = GameObject.FindWithTag("Player");
            _target = new GameObject("target");
            _target.transform.position = _player.transform.position;
            _target.transform.rotation = _target.transform.rotation;
        }

        public void UpdateCamera(Vector2 mouseInput) 
        {
            UpdateTarget(_player);
            AvoidObstacle();
            RotateAroundTarget(mouseInput);
            FollowTarget();
        }

        void RotateAroundTarget(Vector2 mouseDelta) 
        {
        
            _euler.x = _target.transform.eulerAngles.x + mouseDelta.y;
            _euler.y = _target.transform.eulerAngles.y + mouseDelta.x;
            
            _target.transform.rotation = Quaternion.Euler(_euler.x, _euler.y, 0);
            _target.transform.rotation = ClampAngleOnAxis(_target.transform.rotation, 0, -40, 80);

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(-_target.transform.forward), 10 * Time.deltaTime);
        }

        void FollowTarget() 
        {
            _position = _target.transform.position + _cameraOffset + _distance * _target.transform.forward;

            transform.position = Vector3.Lerp(transform.position, _position, 10 * Time.deltaTime);
        }

        void UpdateTarget(GameObject target)
        {
            _target.transform.position = target.transform.position;
        }

        void AvoidObstacle() 
        {
            Ray ray = new Ray(_target.transform.position + _cameraOffset, -transform.forward);
            RaycastHit hit;

            _distance = _desiredDistance;

            if(Physics.Raycast(ray, out hit, _desiredDistance + 0.1f)) 
            {
                _distance = Vector3.Distance(ray.GetPoint(0f), hit.point) - 0.1f;
            }

            Debug.DrawRay(ray.GetPoint(0), ray.direction * _distance);


        }

        Quaternion ClampAngleOnAxis(Quaternion q, int axis, float minAngle, float maxAngle)
        {
            q.x /= q.w;
            q.y /= q.w;
            q.z /= q.w;
            q.w = 1.0f;
    
            var angle = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q[axis]);
    
            angle = Mathf.Clamp(angle, minAngle, maxAngle);
    
            q[axis] = Mathf.Tan(0.5f * Mathf.Deg2Rad * angle);
    
            return q;
        }

    }
}