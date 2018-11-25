using System;

namespace Xunity.Extensions
{
    public static class ActionExtensions
    {
        public static void SafeInvoke(this Action action)
        {
            if (action == null) return;
            action();
        }
    }
}