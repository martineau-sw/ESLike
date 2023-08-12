
using System;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace ESLike.Actor.Skills
{
    [Serializable]
    public class Skill
    {
        [SerializeField]
        string _name;

        [SerializeField]
        int _experience;

        public string Name => _name;
        public int XP 
        {
            get => _experience;
            set => _experience = value;
        }
        public int Level
        {
            get => Mathf.FloorToInt(Mathf.Pow(_experience, 3/4f) / 8f);
        }
    }
}