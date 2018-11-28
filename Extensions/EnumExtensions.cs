using System;

namespace Xunity.Extensions
{
    public static class EnumExtensions
    {
        public static bool ContainsFlag(this Enum keys, Enum flag)
        {
            int keysVal = Convert.ToInt32(keys);
            int flagVal = Convert.ToInt32(flag);
            return (keysVal & flagVal) > 0;
        }
    }
}