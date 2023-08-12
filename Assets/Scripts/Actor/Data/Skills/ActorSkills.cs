
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

        public ActorSkills()
        {
            _skills = new List<Skill>();
        }
    }
}