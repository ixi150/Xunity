using System;
using UnityEngine;

namespace Xunity.Utils
{
    public static class ActionUtils
    {
        public static void Try(Action action)
        {
            try
            {
                action();
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }

        public static void SafeInvoke(Action action)
        {
            if (action != null) action();
        }
    }
}