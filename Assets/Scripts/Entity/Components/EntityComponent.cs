using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ESLike.Entity 
{
    public class EntityComponent : MonoBehaviour
    {
        [SerializeField]
        EntityAttributes _attributes;

        [SerializeField]
        EntityMeters _meters;
        
        [SerializeField]
        EntitySkills _skills;

        [SerializeField]
        EntityMotor _motor;

        public EntityAttributes Attributes => _attributes;
        public EntityMeters Meters => _meters;
        public EntitySkills Skills => _skills;

        float _tickTimer;

        void Start() 
        {
            _motor = GetComponent<EntityMotor>();
            _meters = new EntityMeters(_attributes);
            _skills = new EntitySkills(_attributes);
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