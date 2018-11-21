using UnityEngine;

namespace Xunity.Utils
{
    public static class Vector3Utils
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
    }
}