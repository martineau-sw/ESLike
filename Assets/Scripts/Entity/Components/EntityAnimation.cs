using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ESLike.Entity
{
    public class EntityAnimation : MonoBehaviour
    {
        Animator _animator;
        Rigidbody _rigidbody;

        Vector2 _horizontal = new Vector2();

        EntityMotor _motor;
        PlayerCamera _playerCamera;

        // Start is called before the first frame update
        void Start()
        {
            _animator = GetComponentInChildren<Animator>();
            _rigidbody = GetComponent<Rigidbody>();
            _motor = GetComponent<EntityMotor>();
            _playerCamera = Camera.main.transform.root.GetComponent<PlayerCamera>();
        }

        // Update is called once per frame
        void Update()
        {
            float theta = _playerCamera.transform.eulerAngles.y * Mathf.Deg2Rad;

            _horizontal.x = Mathf.Cos(theta) * _rigidbody.velocity.x + Mathf.Sin(theta) * _rigidbody.velocity.z;
            _horizontal.y = Mathf.Cos(theta) * _rigidbody.velocity.z - Mathf.Sin(theta) * _rigidbody.velocity.x;

            _animator.SetFloat("VelocityXZ", _horizontal.magnitude);
            _animator.SetFloat("VelocityY", _rigidbody.velocity.y);

            _animator.SetBool("HardLanding", _motor.HardLanding);
            _animator.SetBool("Airborne", _motor.Airborne);
        }
    }
}