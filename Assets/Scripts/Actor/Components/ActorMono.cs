
using System.Collections.Generic;
using UnityEngine;

using ESLike.Actor.Extensions;
using ESLike.Actor.Skills;
using System;

namespace ESLike.Actor 
{
    public class ActorMono : ActorMotor, IActorMeters
    {
        [SerializeField] 
        Attributes _attributes;

        [SerializeField]
        Meter _health;
        [SerializeField]
        Meter _breath;
        [SerializeField]
        Meter _focus;
        
        [SerializeField]
        ActorSkills _skills;


        public Vector3 DirectionInput {get; set;}
        public bool SprintInput {get; set;}
        
        public Meter Health => _health;
        public Meter Focus => _focus;
        public Meter Breath => _breath;
        public Attributes Attributes => _attributes;
        public ActorSkills Skills => _skills;

        new void Start() 
        {
            base.Start();

            _attributes = new Attributes(0);

            _health = new Meter(_attributes.GetMaxHealth());
            _breath = new Meter(_attributes.GetMaxBreath());
            _focus = new Meter(_attributes.GetMaxFocus());

            Tick.OnTick += (s, e) => _health.Tick_Regen(_attributes.GetHealthTick());
            Tick.OnTick += (s, e) => _breath.Tick_Regen(_attributes.GetBreathTick());
            Tick.OnTick += (s, e) => _focus.Tick_Regen(_attributes.GetFocusTick());

            _skills = new ActorSkills();
        }

        new void Update() 
        {
            base.Update();

            Sprint = _breath.Value > 0 && SprintInput;
        
            Move(DirectionInput);
        }

        new void LateUpdate() 
        {
            
        }

        void OnApplicationQuit() 
        {
        }
    }
}