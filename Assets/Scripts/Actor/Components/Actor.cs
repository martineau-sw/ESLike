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

        float _tickTimer;

        void Start() 
        {
            _motor = GetComponent<ActorMotor>();
            _meters = new ActorMeters(_attributes);
            _skills = new ActorSkills(_attributes);
        }

        void FixedUpdate()
        {
            UpdateTick();
        }

        void LateUpdate() 
        {
            SprintStamina();
        }

        void SprintStamina() 
        {
            _motor.CanSprint = _meters.Breath.Value > 0;
            _meters.Breath.TickValue = _motor.Sprint ? -10 : 0;
        }

        void UpdateTick()
        {
            if(_tickTimer > 0f) 
            { 
                _tickTimer -= Time.deltaTime;
                return;
            }

            _meters.Tick();
            _tickTimer = 0.3f;
        }
    }
}