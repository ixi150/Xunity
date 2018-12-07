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

        public virtual void Set(T v, object source = null)
        {
            if (!authorizedSources.IsAuthorized(source))
            {
                string error = "Unauthorized set " + v + " attempt by " +
                               (source == null ? "null" : source.ToString());
                Debug.LogError(error, this);
                return;
            }

            if (this.value.Equals(v))
                return;

            this.value = v;
            ValueChanged(v);
            if (changedEvent)
                changedEvent.Raise(v);
        }


        public override string ToString()
        {
            return value == null ? "null" : value.ToString();
        }
    }
}