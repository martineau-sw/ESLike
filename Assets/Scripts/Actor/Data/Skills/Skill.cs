
using System;
using UnityEngine;

namespace ESLike.Actor.Skills
{
    [Serializable]
    public class Skill
    {
        [SerializeField]
        string _name;
        
        [SerializeField]
        string _formula;

        [SerializeField]
        int _experience;

        public string Name => _name;

        public string Formula => _formula;

        public int XP 
        {
            get => _experience;
            set => _experience = value;
        }
        public int Level
        {
            get => Mathf.FloorToInt(Mathf.Pow(_experience, 3/4f) / 8f);
        }

        public Skill(string name, string formula, int experience)
        {
            _name = name;
            _formula = formula;
            _experience = experience;
        }
    }
}