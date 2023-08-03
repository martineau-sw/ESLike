using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ESLike.Entity.Extensions
{
    public static class EntityAttributesExtensions 
    {
        const int METER_BASELINE = 100; 
        const int METER_MULTIPLIER = 5;

        const int ATTRIBUTE_MULTIPLIER_MAJOR = 5;
        const int ATTRIBUTE_MULTIPLIER_MINOR = 2;

        public static int GetMaxHealth(this EntityAttributes entity)
        {
            return METER_BASELINE + METER_MULTIPLIER * entity.Constitution;
        }

        public static int GetMaxBreath(this EntityAttributes entity)
        {
            return METER_BASELINE + METER_MULTIPLIER * entity.Strength;
        }

        public static int GetMaxFocus(this EntityAttributes entity)
        {
            return METER_BASELINE + METER_MULTIPLIER * entity.Intelligence;
        }

        public static int GetHealthTick(this EntityAttributes entity) 
        {
            return 2 + Mathf.FloorToInt(entity.Strength / 2f) + Mathf.FloorToInt(entity.Constitution / 3f);
        }

        public static int GetFocusTick(this EntityAttributes entity) 
        {
            return 5 + Mathf.FloorToInt(entity.Wisdom / 2f);
        }

        public static int GetBreathTick(this EntityAttributes entity)
        {
            return 6 + Mathf.FloorToInt(entity.Dexterity / 2f);
        }

        public static int GetHeavyArmorBase(this EntityAttributes entity)
        {
            return ATTRIBUTE_MULTIPLIER_MAJOR * entity.Strength;
        }

        public static int GetLightArmorBase(this EntityAttributes entity)
        {
            return ATTRIBUTE_MULTIPLIER_MAJOR * entity.Dexterity;
        }

        public static int GetUnArmorBase(this EntityAttributes entity)
        {
            return ATTRIBUTE_MULTIPLIER_MAJOR * entity.Constitution + ATTRIBUTE_MULTIPLIER_MINOR * entity.Dexterity;
        }

        public static int GetHeavyWeaponBase(this EntityAttributes entity)
        {
            return ATTRIBUTE_MULTIPLIER_MAJOR * entity.Strength;
        }

        public static int GetLightWeaponBase(this EntityAttributes entity)
        {
            return ATTRIBUTE_MULTIPLIER_MAJOR * entity.Dexterity;
        }

        public static int GetUnArmBase(this EntityAttributes entity)
        {
            return ATTRIBUTE_MULTIPLIER_MAJOR * entity.Strength + ATTRIBUTE_MULTIPLIER_MINOR * entity.Wisdom;
        }

        public static int GetElementalMagicBase(this EntityAttributes entity)
        {
            return ATTRIBUTE_MULTIPLIER_MAJOR * entity.Wisdom;
        }

        public static int GetWhiteMagicBase(this EntityAttributes entity)
        {
            return ATTRIBUTE_MULTIPLIER_MAJOR * entity.Wisdom;
        }

        public static int GetBlackMagicBase(this EntityAttributes entity)
        {
            return ATTRIBUTE_MULTIPLIER_MAJOR * entity.Intelligence;
        }

        public static int GetRitualMagicBase(this EntityAttributes entity)
        {
            return ATTRIBUTE_MULTIPLIER_MAJOR * entity.Intelligence;
        }

        public static int GetEnchantingBase(this EntityAttributes entity)
        {
            return ATTRIBUTE_MULTIPLIER_MAJOR * entity.Intelligence;
        }

        public static int GetAlchemyBase(this EntityAttributes entity)
        {
            return ATTRIBUTE_MULTIPLIER_MAJOR * entity.Intelligence;
        }
    }
}