using UnityEngine;

namespace Orbia.Utils
{
    public static class VectorUtils
    {
        public static float InverseLerpUnclamped(Vector3 a, Vector3 b, Vector3 value)
        {
            var ab = b - a;
            var av = value - a;
            return Vector3.Dot(av, ab) / Vector3.Dot(ab, ab);
        }

        public static float InverseLerp(Vector3 a, Vector3 b, Vector3 value)
        {
            return Mathf.Clamp01(InverseLerpUnclamped(a, b, value));
        }
        
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