using UnityEngine;

namespace Xunity.Extensions
{
    public static class Vector3Extensions
    {
        public static Vector3 Modified(this Vector3 vector, float? x = null, float? y = null, float? z = null)
        {
            if (x.HasValue) vector.x = x.Value;
            if (y.HasValue) vector.y = y.Value;
            if (z.HasValue) vector.z = z.Value;
            return vector;
        }
    }
}