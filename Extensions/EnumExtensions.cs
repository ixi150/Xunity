using System;

namespace Xunity.Extensions
{
    public static class EnumExtensions
    {
        public static bool ContainsFlag(this Enum keys, Enum flag)
        {
            ulong keysVal = Convert.ToUInt64(keys);
            ulong flagVal = Convert.ToUInt64(flag);
            return (keysVal & flagVal) == flagVal;
        }
    }
}