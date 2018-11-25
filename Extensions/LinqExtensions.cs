using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Xunity.Extensions
{
    public static class LinqExtensions
    {
        public static Vector3 Sum(this IEnumerable<Vector3> enumerable)
        {
            return enumerable.Aggregate(Sum);
        }

        public static Vector3 Average(this IEnumerable<Vector3> enumerable)
        {
            var sum = Vector3.zero;
            var i = 0;
            foreach (var item in enumerable)
            {
                sum += item;
                i++;
            }

            return i > 0 ? sum / i : default(Vector3);
        }

        public static Vector3 SafeAverage(this IEnumerable<Vector3> enumerable)
        {
            var array = enumerable.ToArray();
            return array.Length > 0
                ? array.Aggregate((current, item) => current + item / array.Length)
                : default(Vector3);
        }

        public static Vector2 Sum(this IEnumerable<Vector2> enumerable)
        {
            return enumerable.Aggregate(Sum);
        }

        public static Vector2 Average(this IEnumerable<Vector2> enumerable)
        {
            var sum = Vector2.zero;
            var i = 0;
            foreach (var item in enumerable)
            {
                sum += item;
                i++;
            }

            return i > 0 ? sum / i : default(Vector2);
        }

        public static Vector2 SafeAverage(this IEnumerable<Vector2> enumerable)
        {
            var array = enumerable.ToArray();
            return array.Length > 0
                ? array.Aggregate((current, item) => current + item / array.Length)
                : default(Vector2);
        }

        public static IEnumerable<T> NotNull<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.Where(NotNull);
        }

        static Vector3 Sum(Vector3 a, Vector3 b)
        {
            return a + b;
        }

        static Vector2 Sum(Vector2 a, Vector2 b)
        {
            return a + b;
        }

        static bool NotNull<T>(T item)
        {
            return item != null;
        }
    }
}