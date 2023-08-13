
using ESLike.Actor.Skills;
using Unity.Mathematics;
using UnityEngine;

namespace ESLike.Actor.Skills
{
    public static class SkillExtensions 
    {

        public static ushort ConvertLevelToXP(int level)
        {
            return (ushort)Mathf.CeilToInt(Mathf.Pow(level, 4/3f) * 16);
        }
    }
}