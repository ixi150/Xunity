using System;
using UnityEngine;
using Xunity.Authorization;
using Xunity.ScriptableEvents;

namespace Xunity.ScriptableVariables
{
    public abstract class ScriptableVariable<T> : ScriptableVariableBase
    {
        [SerializeField] T value;
        [SerializeField] VariableEvent<T> changedEvent;
        [SerializeField] AuthorizedSources authorizedSources;

        public event Action<T> ValueChanged = (_) => { };

        public static implicit operator T(ScriptableVariable<T> variable)
        {
            return variable ? variable.value : default(T);
        }

        public void Set(T value, object source = null)
        {
            if (!authorizedSources.IsAuthorized(source))
            {
                string error = "Unauthorized set " + value + " attempt by " +
                               (source == null ? "null" : source.ToString());
                Debug.LogError(error, this);
                return;
            }

            if (this.value.Equals(value))
                return;

            this.value = value;
            ValueChanged(value);
            if (changedEvent)
                changedEvent.Raise(value);
        }


        public override string ToString()
        {
            return value.ToString();
        }
    }
}