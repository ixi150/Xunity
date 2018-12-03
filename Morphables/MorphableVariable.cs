using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Xunity.Sets;

namespace Xunity.Morphables
{
    public abstract class MorphableBase{}
    
    [Serializable] public class IntMorph : MorphableVariable<int>{}
    [Serializable] public class StringMorph : MorphableVariable<string>{}
    [Serializable] public class SetMorph : MorphableVariable<SetCollection>{}

    [Serializable]
    public class MorphableVariable<T> : MorphableBase, IEnumerable<T>
    {
        [SerializeField] T[] array = {default(T)};
        [SerializeField] bool isArray = false;

        public static implicit operator T(MorphableVariable<T> variable)
        {
            return variable != null ? variable.array[0] : default(T);
        }

        public IEnumerator<T> GetEnumerator()
        {
            int size = isArray ? array.Length : 1;
            for (var i = 0; i < size; i++)
                yield return array[i];
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        class MorphEnumerator : IEnumerator<T>
        {
            int index = -1;
            readonly int size;
            readonly T[] array;

            public MorphEnumerator(T[] array, int size)
            {
                this.array = array;
                this.size = size;
            }

            public bool MoveNext()
            {
                index++;
                return index < size;
            }

            public void Reset()
            {
                index = -1;
            }

            public T Current
            {
                get { return array[index]; }
            }

            object IEnumerator.Current
            {
                get { return Current; }
            }

            public void Dispose()
            {
            }
        }
    }
}