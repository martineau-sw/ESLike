using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ESLike.Actor 
{
    public class Actor : MonoBehaviour
    {
        [SerializeField]
        ActorAttributes _attributes;

        [SerializeField]
        ActorMeters _meters;
        
        [SerializeField]
        ActorSkills _skills;

        [SerializeField]
        ActorMotor _motor;

        public ActorAttributes Attributes => _attributes;
        public ActorMeters Meters => _meters;
        public ActorSkills Skills => _skills;

        void Start() 
        {
            _motor = GetComponent<ActorMotor>();
            _meters = new ActorMeters(_attributes);
            _skills = new ActorSkills(_attributes);
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
            _motor.CanSprint = _meters.Breath.Value > 0;
        }
    }
}