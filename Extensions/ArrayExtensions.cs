﻿using UnityEngine;

namespace Xunity.Extensions
{
    public static class ArrayExtensions
    {
        public static T GetRandomElement<T>(this T[] container)
        {
            return container[Random.Range(0, container.Length - 1)];
        }
    }
}