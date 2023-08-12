using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ESLike.Actor;
using ESLike.Actor.Skills;
using System;
using System.Linq.Expressions;

namespace ESLike.Serialization
{
    public static class Serializer
    {
        public static readonly string PATH = "./Assets/Config/";
    }

    public static class Skills 
    {
        public static List<Skill> FromTXT(string fileName)
        {
            // <skilltag>, <expression>, <additionalXP>

            List<Skill> skills = new List<Skill>();
            string[] lines = File.ReadAllLines($"{Serializer.PATH}{fileName}.txt");

            foreach(string line in lines)
            {
                string[] tokens = line.Split(", ");
                string skillTag = tokens[0];
                string expression = tokens[1];
                string additionalXP = tokens[2];
                Skill skill = new Skill(skillTag, expression, int.Parse(additionalXP));
                skills.Add(skill);
            }
            
            return skills;
        }
    }

    public static class Actor 
    {
        public static ActorMono FromJSON(string fileName)
        {
            return JsonUtility.FromJson<ActorMono>(File.ReadAllText($"{Serializer.PATH}{fileName}.json"));
        }

        public static void ToJSON(this ActorMono attributes, string fileName) 
        {
            string json = JsonUtility.ToJson(attributes, true);
            string file = $"{Serializer.PATH}{fileName}.json";

            File.WriteAllText(file, json);
        }
    }
}
