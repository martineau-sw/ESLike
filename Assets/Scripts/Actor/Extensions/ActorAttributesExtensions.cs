using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ESLike.Actor.Extensions
{
    public static class AttributesExtensions 
    {
        const int METER_BASELINE = 100; 
        const int METER_MULTIPLIER = 5;

        const int ATTRIBUTE_MULTIPLIER_MAJOR = 5;
        const int ATTRIBUTE_MULTIPLIER_MINOR = 2;

        public static int GetMaxHealth(this Attributes entity)
        {
            return METER_BASELINE + METER_MULTIPLIER * entity.Constitution;
        }

        public static int GetMaxBreath(this Attributes entity)
        {
            return METER_BASELINE + METER_MULTIPLIER * entity.Strength;
        }

        public static int GetMaxFocus(this Attributes entity)
        {
            return METER_BASELINE + METER_MULTIPLIER * entity.Intelligence;
        }

        public static int GetHealthTick(this Attributes entity) 
        {
            return 2 + Mathf.FloorToInt(entity.Strength / 2f) + Mathf.FloorToInt(entity.Constitution / 3f);
        }

        public static int GetFocusTick(this Attributes entity) 
        {
            return 5 + Mathf.FloorToInt(entity.Wisdom / 2f);
        }

        public static int GetBreathTick(this Attributes entity)
        {
            return 6 + Mathf.FloorToInt(entity.Dexterity / 2f);
        }
    }
}