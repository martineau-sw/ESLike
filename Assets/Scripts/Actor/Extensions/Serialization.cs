using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ESLike.Actor;

namespace ESLike.Serialization
{
    public static class Serializer
    {
        public static readonly string PATH = "./Assets/JSON/";
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
