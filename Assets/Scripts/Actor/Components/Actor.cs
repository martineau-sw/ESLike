using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ESLike.Actor 
{
    public class ActorMono : MonoBehaviour
    {
        [SerializeField]
        ActorAttributes _attributes;

        [SerializeField]
        ActorMeters _meters;
        
        [SerializeField]
        ActorSkills _skills;

        [SerializeField]
        ActorMotor _motor;

        public Vector3 Direction;
        public bool Jump;
        public bool Sprint;
        

        public ActorAttributes Attributes => _attributes;
        public ActorMeters Meters => _meters;
        public ActorSkills Skills => _skills;

        void Start() 
        {
            _motor = GetComponent<ActorMotor>();
            _meters = new ActorMeters(_attributes);
            _skills = new ActorSkills(_attributes);
        }

        void Update() 
        {
            _motor.Move(Direction);
            _motor.Jump(Jump);
        }

        void FixedUpdate()
        {
        }

        void LateUpdate() 
        {
            SprintStamina();
        }

        void SprintStamina() 
        {
            
        }
    }
}