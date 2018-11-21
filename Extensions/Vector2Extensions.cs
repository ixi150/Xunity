using UnityEngine;

namespace Xunity.Extensions
{
    public static class Vector2Extensions
    {
        public static Vector2 Modified(this Vector2 vector, float? x = null, float? y = null)
        {
            if (x.HasValue) vector.x = x.Value;
            if (y.HasValue) vector.y = y.Value;
            return vector;
        }
    }
}