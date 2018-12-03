using UnityEngine;

namespace Xunity.Extensions
{
    public static class FloatExtensions
    {
        public static float LimitedTo(this float f, float max)
        {
            return Mathf.Sign(f) * Mathf.Min(Mathf.Abs(f), max);
        }
    }
}