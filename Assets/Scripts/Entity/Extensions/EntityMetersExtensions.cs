using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ESLike.Entity.Extensions
{
    public static class EntityMetersExtensions 
    {
        public static float HealthNormalized(this EntityMeters meters) 
        {
            return (float)meters.Health.Value / meters.Health.Max;
        }

        public static float BreathNormalized(this EntityMeters meters) 
        {
            return (float)meters.Breath.Value / meters.Breath.Max;
        }

        public static float FocusNormalized(this EntityMeters meters) 
        {
            return (float)meters.Focus.Value / meters.Focus.Max;
        }
    }
}
