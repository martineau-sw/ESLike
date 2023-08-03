using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ESLike.Entity
{
    public static class EntityUtility
    {
        public static int Clamp(int value, int limit)
        {
            return 
                (value >= limit) ? limit : 
                (value <= -limit) ? -limit : 
                value; 
        }

        public static int Clamp(int value, int min, int max)
        {
            return 
                (value >= max) ? max : 
                (value <= min) ? min : 
                value; 
        }
    }
}