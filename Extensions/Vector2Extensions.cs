using UnityEngine;

namespace Xunity.Extensions
{
    public static class Vector2Extensions
    {
        public static Vector2 Modified(this Vector2 vector, float x = float.NaN, float y = float.NaN)
        {
            if (float.IsNaN(x)) vector.x = x;
            if (float.IsNaN(y)) vector.y = y;
            return vector;
        }

        public static Vector2 Multiply(this Vector2 a, Vector2 b)
        {
            a.Scale(b);
            return a;
        }
        
        public static Vector2 LimitedTo(this Vector2 v, Vector2 max)
        {
            v.x = v.x.LimitedTo(max.x);
            v.y = v.y.LimitedTo(max.y);
            return v;
        }
    }
}