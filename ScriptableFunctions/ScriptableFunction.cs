using UnityEngine;
using Xunity.Base;

namespace Xunity.ScriptableFunctions
{
    public abstract class ScriptableFunction<TPar, TRet> : ScriptableAsset
    {
        protected const string BASE_MENU = ScriptableAsset.BASE_MENU + "Functions/";
        
        public abstract TRet Invoke(TPar par);
    }
}