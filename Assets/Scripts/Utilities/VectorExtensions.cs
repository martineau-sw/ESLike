using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ESLike.Utilities
{
    public static class VectorExtensions 
    {
        public static Vector2 Rotate(this Vector2 vector, float theta) 
        {
            theta *= Mathf.Deg2Rad;
            
            return new Vector2(
                Mathf.Cos(theta) * vector.x + Mathf.Sin(theta) * vector.y,
                Mathf.Cos(theta) * vector.y - Mathf.Sin(theta) * vector.x);
        }

        public static Vector3 Vector2ToXZ(this Vector2 vector)
        {
            return new Vector3(vector.x, 0, vector.y);
        }

        public static Vector3 ClampXZ(this Vector3 vector, float min, float max) 
        {
            vector.x = Mathf.Clamp(vector.x, min, max);
            vector.z = Mathf.Clamp(vector.z, min, max);

            return vector;
        }
    }
}
    
