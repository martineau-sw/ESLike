using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ESLike.Actor.Extensions
{
    public static class ActorMetersExtensions 
    {
        public static float HealthNormalized(this ActorMeters meters) 
        {
            return (float)meters.Health.Value / meters.Health.Max;
        }

        public static float BreathNormalized(this ActorMeters meters) 
        {
            return (float)meters.Breath.Value / meters.Breath.Max;
        }

        public static float FocusNormalized(this ActorMeters meters) 
        {
            return (float)meters.Focus.Value / meters.Focus.Max;
        }
    }
}
