
using UnityEngine;

using ESLike.Actor.Extensions;
using ESLike.Actor.Skills;
using ESLike.Serialization;

namespace ESLike.Actor 
{
    public class ActorMono : MonoBehaviour, IActorMeters
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

        void Start() 
        {
            _attributes = new Attributes(1, 2, 3, 0, -1, -2);
            _skills = new ActorSkills(_attributes);


            _health = new Meter(_attributes.GetMaxHealth());
            _breath = new Meter(_attributes.GetMaxBreath());
            _focus = new Meter(_attributes.GetMaxFocus());

            Tick.OnTick += (s, e) => _health.Tick_Regen(_attributes.GetHealthTick());
            Tick.OnTick += (s, e) => _breath.Tick_Regen(_attributes.GetBreathTick());
            Tick.OnTick += (s, e) => _focus.Tick_Regen(_attributes.GetFocusTick());
        }

        void OnApplicationQuit() 
        {
            this.ToJSON("player");
        }
    }
}