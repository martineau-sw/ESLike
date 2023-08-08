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

        public event EventHandler<OnChangeEventArgs> OnEmpty;
        public event EventHandler<OnChangeEventArgs> OnChange;
        public event EventHandler<OnChangeEventArgs> OnFull;
    
        public int Value 
        {
            get => _value;
            set 
            {
                if(value == _value) return;

                value = Mathf.Clamp(value, 0, _max);

                if(value == _max) 
                {
                    OnFull?.Invoke(this, new OnChangeEventArgs(_value, value));
                    _value = _max;
                    return;
                }

                if(value == 0)
                {
                    OnEmpty?.Invoke(this, new OnChangeEventArgs(_value, value));
                    _value = 0;
                    return;
                }

                OnChange?.Invoke(this, new OnChangeEventArgs(_value, value));
                _value = value;
                
            }
        }

        public int Max
        {
            get => _max;
            set => _max = value < 1 ? 1 : value;
        }

        public float Normal => Value / Max;
        public Meter(int max) 
        {
            _value = max;
            _max = max;
        }

        public void Tick_Regen(int value) 
        {
            Value += value;
        }

        public class OnChangeEventArgs : EventArgs 
        {

            readonly int _prev;
            readonly int _current;       

            public int Previous => _prev;
            public int Current => _current;
            public int Difference => _current - _prev;
            public int Distance => Mathf.Abs(_current - _prev);

            public OnChangeEventArgs(int prev, int current) 
            {
                _prev = prev;
                _current = current;
            }
         }
    }


}