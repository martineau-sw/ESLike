
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ESLike.Actor.Skills 
{
    [Serializable]
    public class ActorSkills 
    {
        [SerializeReference]
        List<Skill> _skills;

        public ActorSkills(Attributes attributes)
        {
            _skills = Serialization.Skills.FromTXT("skills");

            foreach(Skill skill in _skills) 
            {
                skill.XP += SkillParser.Evaluate(attributes, skill.Formula);
            }
        }
    }
}