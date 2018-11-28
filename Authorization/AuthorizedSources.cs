using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Xunity.ScriptableVariables;

namespace Xunity.Authorization
{
    [Serializable]
    public class AuthorizedSources
    {
        [SerializeField] bool restrictAccess;
        [SerializeField] List<SerializableMonoscript> authorizedSources;

        public bool IsAuthorized(object source)
        {
            if (!restrictAccess)
                return true;

            if (source == null)
                return false;

            string sourceType = source.GetType().ToString();
            return authorizedSources.Any(_ => _ == sourceType);
        }
    }
}