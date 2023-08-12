

namespace ESLike.Actor
{
    public interface IActorMeters
    {
        public Meter Health { get; }
        public Meter Focus { get; } 
        public Meter Breath { get; }
    }
}