using System;
using UnityEngine;
using Xunity.ScriptableVariables;

namespace Xunity.ReferenceVariables
{
    [Serializable]
    public class ReferenceVariable<TRef, TVal, TClass>
        where TRef : ScriptableVariable<TVal>
        where TClass : ReferenceVariable<TRef, TVal, TClass>, new()
    {
        [SerializeField] TRef reference;
        [SerializeField] TVal value;
        [SerializeField] bool useConstant;

        public static implicit operator TVal(ReferenceVariable<TRef, TVal, TClass> variable)
        {
            return variable.useConstant ? variable.value : variable.reference;
        }

        public static TClass New(bool useConstant = false, TVal value = default(TVal),
            TRef reference = default(TRef))
        {
            return new TClass
            {
                useConstant = useConstant,
                value = value,
                reference = reference,
            };
        }

//        public ReferenceVariable(bool useConstant = false, TVal value = default(TVal), TRef reference = default(TRef))
//        {
//            this.useConstant = useConstant;
//            this.value = value;
//            this.reference = reference;
//        }
    }
}