using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Xunity.ScriptableEvents;
using Object = UnityEngine.Object;

namespace Xunity.ScriptableVariables
{
    public abstract class ScriptableVariable<T> : ScriptableObject
    {
        [SerializeField] T value;
        [SerializeField] GameEvent changedEvent;
        [SerializeField] bool restrictAccess;
        [SerializeField] List<SerializableMonoscript> authorizedSources;

        public event Action<T> ValueChanged = (_) => { };

        public static implicit operator T(ScriptableVariable<T> variable)
        {
            return variable ? variable.value : default(T);
        }

        public void Set(T value, Object source = null)
        {
            if (restrictAccess && !IsAuthorized(source))
            {
                string error = "Unauthorized set " + value + " attempt by " + (source ? source.ToString() : "null");
                Debug.LogError(error, source);
                return;
            }

            if (this.value.Equals(value))
                return;

            this.value = value;
            ValueChanged(value);
            if (changedEvent)
                changedEvent.Raise();
        }

        public bool IsAuthorized(Object source)
        {
            if (source == null)
                return false;

            string sourceType = source.GetType().ToString();
            return authorizedSources.Any(_ => _ == sourceType);
        }

        public override string ToString()
        {
            return value.ToString();
        }
    }
}