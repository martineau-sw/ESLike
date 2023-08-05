using System;
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
            _health = new Meter(attributes.GetMaxHealth());
            _breath = new Meter(attributes.GetMaxBreath());
            _focus = new Meter(attributes.GetMaxFocus());

            Tick.OnTick += (s, e) => _health.Tick_Regen(attributes.GetHealthTick());
            Tick.OnTick += (s, e) => _breath.Tick_Regen(attributes.GetBreathTick());
            Tick.OnTick += (s, e) => _focus.Tick_Regen(attributes.GetFocusTick());
        }
    }

    [System.Serializable]
    public class Meter
    {
        [SerializeField]
        int _value;
        [SerializeField]
        int _max;

        public event EventHandler OnEmpty;
        public event EventHandler OnRegen;
        public event EventHandler OnDegen;
        public event EventHandler OnFull;
    
        public int Value 
        {
            get => _value;
            set 
            {
                // Clamp min
                if(_value <= 0f) 
                {
                    if(value <= 0f) 
                    {
                        _value = 0f;
                        return;
                    }
                    
                }

                // Clamp max
                if(_value >= max) 
                {
                    if(value >= _max) 
                    {
                        _value = _max;
                        return;
                    }
                }

                if(value >= _max) 
                {
                    OnFull?.Invoke(this, null);
                    return;
                }

                if(value <= 0f)
                {
                    OnEmpty?.Invoke(this, null);
                    return;
                }

                if(value > _value) OnRegen?.Invoke(this, null);
                if(value < _value) OnDegen?.Invoke(this, null);
                _value = value;
                
            }
        }

        public int Max
        {
            get => _max;
            set => _max = value < 1 ? 1 : value;
        }

        public Meter(int max) 
        {
            _value = max;
            _max = max;
        }

        public void Tick_Regen(int value) 
        {
            Value += value;
        }
    }


}