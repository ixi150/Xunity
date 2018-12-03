using UnityEngine;

namespace Xunity.Extensions
{
    public static class Vector3Extensions
    {
        public static Vector3 Modified(this Vector3 vector,
            float x = float.NaN,
            float y = float.NaN,
            float z = float.NaN)
        {
            if (!float.IsNaN(x)) vector.x = x;
            if (!float.IsNaN(y)) vector.y = y;
            if (!float.IsNaN(z)) vector.z = z;
            return vector;
        }

        public static Vector3 Multiply(this Vector3 a, Vector3 b)
        {
            a.Scale(b);
            return a;
        }
    }
}