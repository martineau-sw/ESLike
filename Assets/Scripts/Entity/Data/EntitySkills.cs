using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ESLike.Entity.Extensions;

namespace ESLike.Entity
{
    [System.Serializable]
    public class EntitySkills 
    {
        [SerializeField]
        Skill _heavyArmor;
        [SerializeField]
        Skill _lightArmor;
        [SerializeField]
        Skill _unarmored;
        [SerializeField]
        Skill _heavyWeapon;
        [SerializeField]
        Skill _lightWeapon;
        [SerializeField]
        Skill _unarmed;
        [SerializeField]
        Skill _elementalMagic;
        [SerializeField]
        Skill _blackMagic;
        [SerializeField]
        Skill _whiteMagic;
        [SerializeField]
        Skill _ritualMagic;
        [SerializeField]
        Skill _enchanting;
        [SerializeField]
        Skill _alchemy;

        public EntitySkills(EntityAttributes attributes)
        {
            _heavyArmor = new Skill(attributes.GetHeavyArmorBase());
            _lightArmor = new Skill(attributes.GetLightArmorBase());
            _unarmored = new Skill(attributes.GetUnArmorBase());
            _heavyWeapon = new Skill(attributes.GetHeavyWeaponBase());
            _lightWeapon = new Skill(attributes.GetLightWeaponBase());
            _unarmed = new Skill(attributes.GetUnArmBase());
            _elementalMagic = new Skill(attributes.GetElementalMagicBase());
            _blackMagic = new Skill(attributes.GetBlackMagicBase());
            _whiteMagic = new Skill(attributes.GetWhiteMagicBase());
            _ritualMagic = new Skill(attributes.GetRitualMagicBase());
            _enchanting = new Skill(attributes.GetEnchantingBase());
            _alchemy = new Skill(attributes.GetAlchemyBase());
        }
    }

    [System.Serializable]
    public class Skill 
    {
        [SerializeField]
        ushort experience;

        public ushort XP 
        {
            get => experience;
            set => EntityUtility.Clamp(value, 0, ushort.MaxValue);
        }

        public int Level
        {
            get => Mathf.FloorToInt(Mathf.Pow(experience, 3/4f) / 8f);
        }

        public Skill(int level) 
        {
            experience = GetExperienceFromLevel(level);
        }

        public ushort GetExperienceFromLevel(int level)
        {
            return (ushort)Mathf.CeilToInt(Mathf.Pow(level, 4/3f) * 16);
        }
    }
}