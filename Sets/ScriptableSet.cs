using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Xunity.Sets
{
    public abstract class ScriptableSet : ScriptableObject
    {
        public abstract IEnumerable<Object> Objects { get; }
        public abstract Type Type { get; }
    }

    public abstract class ScriptableSet<T> : ScriptableSet where T : Object
    {
        readonly List<T> items = new List<T>();

        public override Type Type
        {
            get { return typeof(T); }
        }

        public override IEnumerable<Object> Objects
        {
            get
            {
                foreach (var o in items) 
                    yield return o;
            }
        }

        public IEnumerable<T> Items
        {
            get { return items; }
        }

        public int Count
        {
            get { return items.Count; }
        }

        public void Add(T thing)
        {
            if (!items.Contains(thing))
                items.Add(thing);
        }

        public void Remove(T thing)
        {
            items.Remove(thing);
        }
    }
}