using System;
using UnityEngine;
using Xunity.ScriptableVariables;

namespace Xunity.ScriptableReferences
{
    [Serializable]
    public class VariableReference<TRef, TVal, TClass> : VariableReferenceBase
        where TRef : ScriptableVariable<TVal>
        where TClass : VariableReference<TRef, TVal, TClass>, new()
    {
        [SerializeField] TRef reference;
        [SerializeField] TVal value;
        [SerializeField] bool useConstant;

        public static implicit operator TVal(VariableReference<TRef, TVal, TClass> variable)
        {
            return variable == null ? default(TVal) : variable.Value;
        }

        public TVal Value
        {
            get { return useConstant ? value : reference; }
        }

        public static TClass New
        (
            bool useConstant = false,
            TVal value = default(TVal),
            TRef reference = default(TRef)
        )
        {
            return new TClass
            {
                useConstant = useConstant,
                value = value,
                reference = reference,
            };
        }
    }
}