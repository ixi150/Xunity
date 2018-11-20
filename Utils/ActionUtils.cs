using System;
using UnityEngine;

namespace Orbia.Utils
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
                Debug.LogWarning(e);
            }
        }
    }
}