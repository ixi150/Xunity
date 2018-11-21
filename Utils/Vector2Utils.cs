using UnityEngine;

namespace Xunity.Utils
{
    public static class Vector2Utils
    {
        public static float InverseLerpUnclamped(Vector2 a, Vector2 b, Vector2 value)
        {
            var ab = b - a;
            var av = value - a;
            return Vector2.Dot(av, ab) / Vector2.Dot(ab, ab);
        }

        public static float InverseLerp(Vector2 a, Vector2 b, Vector2 value)
        {
            return Mathf.Clamp01(InverseLerpUnclamped(a, b, value));
        }
    }
}