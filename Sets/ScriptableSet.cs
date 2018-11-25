using System.Collections.Generic;
using UnityEngine;

namespace Xunity.Sets
{
    public abstract class ScriptableSet<T> : ScriptableObject
    {
        readonly List<T> items = new List<T>();

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