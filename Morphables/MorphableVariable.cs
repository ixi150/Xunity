using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Xunity.Morphables
{
    [Serializable]
    public class IntMorph : MorphableVariable<int>
    {
    }
    
    [Serializable]
    public class StringMorph : MorphableVariable<string>
    {
    }

    public class MorphableVariable<T> : MorphableBase, IEnumerable<T>
    {
        [SerializeField] T[] array = {default(T)};
        [SerializeField] bool isArray = false;

        public IEnumerator<T> GetEnumerator()
        {
            return new MorphEnumerator(array, isArray ? array.Length : 1);
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