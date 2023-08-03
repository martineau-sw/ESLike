using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ESLike.Actor.Extensions;

namespace ESLike.Actor
{
    [System.Serializable]
    public class ActorMeters
    {
        [SerializeField]
        Meter _health;
        [SerializeField]
        Meter _focus;
        [SerializeField]
        Meter _breath;
        
        public Meter Health => _health;
        public Meter Focus => _focus;
        public Meter Breath => _breath;

        public ActorMeters(ActorAttributes attributes)
        {
            _health = new Meter(attributes.GetMaxHealth(), attributes.GetHealthTick());
            _breath = new Meter(attributes.GetMaxBreath(), attributes.GetBreathTick());
            _focus = new Meter(attributes.GetMaxFocus(), attributes.GetFocusTick());
        }

        void SprintTick() 
        {
            _breath.TickValue = -5;
        }

        public void Tick() 
        {
            _health.Tick();
            _breath.Tick();
            _focus.Tick();
        }
    }

    [System.Serializable]
    public class Meter
    {
        [SerializeField]
        int _value;
        int _regen;
        int _tickValue;
        [SerializeField]
        int _max;
    
        public int Value 
        {
            get => _value;
            set => _value = ActorUtility.Clamp(value, 0, _max);
        }

        public int Regen 
        {
            get => _regen;
            set => _regen = value < 0 ? 0 : value;
        }

        public int TickValue
        {
            get => _tickValue;
            set => _tickValue = value;
        }

        public int Max
        {
            get => _max;
            set => _max = value < 1 ? 1 : value;
        }

        public Meter(int max, int regen) 
        {
            _regen = regen;
            _value = max;
            _max = max;
        }

        public void Tick() 
        {
            Value += _regen + _tickValue;
        }
    }


}