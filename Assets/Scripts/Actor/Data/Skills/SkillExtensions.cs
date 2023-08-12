
using ESLike.Actor.Skills;
using Unity.Mathematics;
using UnityEngine;

namespace ESLike.Actor.Skills
{
    public static class SkillExtensions 
    {
        public static int GetHeavyArmorFromAttributes(this Skill skill, Attributes attributes)
        {
            return skill.Name == "heavyArmor" ? DefaultValues.ATTRIBUTE_MOD_1 * attributes.Strength : -1;
        }

        public static int GetLightArmorFromAttributes(this Skill skill, Attributes attributes)
        {
            return skill.Name == "lightArmor" ? DefaultValues.ATTRIBUTE_MOD_1 * attributes.Dexterity : -1;
        }

        public static int GetUnarmoredFromAttributes(this Skill skill, Attributes attributes)
        {
            return skill.Name == "unarmored" ? DefaultValues.ATTRIBUTE_MOD_1 * attributes.Constitution + DefaultValues.ATTRIBUTE_MOD_2 * attributes.Dexterity : -1;
        }

        public static int GetHeavyWeaponFromAttributes(this Skill skill, Attributes attributes)
        {
            return skill.Name == "heavyWeapon" ? DefaultValues.ATTRIBUTE_MOD_1 * attributes.Strength : -1;
        }

        public static int GetLightWeaponFromAttributes(this Skill skill, Attributes attributes)
        {
            return skill.Name == "lightWeapon" ? DefaultValues.ATTRIBUTE_MOD_1 * attributes.Strength : -1;
        }

        public static int GetUnarmedFromAttributes(this Skill skill, Attributes attributes)
        {
            return skill.Name == "unarmed" ? DefaultValues.ATTRIBUTE_MOD_1 * attributes.Strength + DefaultValues.ATTRIBUTE_MOD_2 * attributes.Wisdom : -1;
        }

        public static int GetElementalFromAttributes(this Skill skill, Attributes attributes)
        {
            return skill.Name == "elemental" ? DefaultValues.ATTRIBUTE_MOD_1 * attributes.Wisdom : -1;
        }

        public static int GetWhiteFromAttributes(this Skill skill, Attributes attributes)
        {
            return skill.Name == "white" ? DefaultValues.ATTRIBUTE_MOD_1 * attributes.Wisdom : -1;
        }

        public static int GetBlackFromAttributes(this Skill skill, Attributes attributes)
        {
            return skill.Name == "black" ? DefaultValues.ATTRIBUTE_MOD_1 * attributes.Intelligence : -1;
        }

        public static int GetRitualFromAttributes(this Skill skill, Attributes attributes)
        {
            return skill.Name == "ritual" ? DefaultValues.ATTRIBUTE_MOD_1 * attributes.Intelligence : -1;
        }

        public static int GetEnchantingFromAttributes(this Skill skill, Attributes attributes)
        {
            return skill.Name == "enchanting" ? DefaultValues.ATTRIBUTE_MOD_1 * attributes.Intelligence : -1;
        }

        public static int GetAlchemyFromAttributes(this Skill skill, Attributes attributes)
        {
            return skill.Name == "alchemy" ? DefaultValues.ATTRIBUTE_MOD_1 * attributes.Intelligence : -1;
        }

        public static ushort ConvertLevelToXP(int level)
        {
            return (ushort)Mathf.CeilToInt(Mathf.Pow(level, 4/3f) * 16);
        }
    }
}