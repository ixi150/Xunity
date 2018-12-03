using System;
using UnityEngine;
using Xunity.ScriptableVariables;

namespace Xunity.ScriptableReferences
{
    [Serializable]
    public class Vector2Reference : VariableReference<Vector2Variable, Vector2, Vector2Reference> { }
    
    [Serializable]
    public class Vector3Reference : VariableReference<Vector3Variable, Vector3, Vector3Reference> { }
}