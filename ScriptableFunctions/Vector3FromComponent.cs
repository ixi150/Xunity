using System;
using System.Collections.Generic;
using UnityEngine;
using Xunity.Extensions;
using Xunity.ScriptableReferences;
using Xunity.Utils;
using Object = UnityEngine.Object;

namespace Xunity.ScriptableFunctions
{
    [CreateAssetMenu(menuName = BASE_MENU + "Vector3FromComponent")]
    public class Vector3FromComponent : ScriptableFunction<Component, Vector3>
    {
        [SerializeField] Method method;
        [SerializeField] FloatReference scale = FloatReference.New(true, 1);
        [SerializeField] Vector3Reference axisScale = Vector3Reference.New(true, Vector3.one);

        readonly Dictionary<Component, Object> cache = new Dictionary<Component, Object>();

        enum Method
        {
            Position = 0,
            LocalPosition = 1,

            RbVelocity = 10,
            Rb2Velocity = 11,
        }

        public override Vector3 Invoke(Component key)
        {
            return GetVector3(key).Multiply(axisScale) * scale;
        }

        Vector3 GetVector3(Component key)
        {
            switch (method)
            {
                case Method.Position:
                    return GetValueFromCache<Transform>(key, GetPosition);
                case Method.LocalPosition:
                    return GetValueFromCache<Transform>(key, GetLocalPosition);
                case Method.RbVelocity:
                    return GetValueFromCache<Rigidbody>(key, GetRbVelocity);
                case Method.Rb2Velocity:
                    return GetValueFromCache<Rigidbody2D>(key, GetRb2Velocity);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        static Vector3 GetPosition(Transform transform)
        {
            return transform.position;
        }

        static Vector3 GetLocalPosition(Transform transform)
        {
            return transform.localPosition;
        }

        static Vector3 GetRbVelocity(Rigidbody rb)
        {
            return rb.velocity;
        }

        static Vector3 GetRb2Velocity(Rigidbody2D rb2)
        {
            return rb2.velocity;
        }

        Vector3 GetValueFromCache<T>(Component key, Func<T, Vector3> getter) where T : Object
        {
            var cached = GetCachedComponent<T>(key);
            return cached ? getter(cached) : default(Vector3);
        }

        T GetCachedComponent<T>(Component key) where T : Object
        {
            Object cachedValue;
            T component;
            if (cache.TryGetValue(key, out cachedValue))
            {
                component = cachedValue as T;
                if (component == null)
                    cache.Remove(key);
                else
                    return component;
            }

            component = key.GetComponent<T>();
            if (component == null)
                return default(T);
            cache.Add(key, component);
            return component;
        }
    }
}