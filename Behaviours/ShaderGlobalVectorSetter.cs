using UnityEngine;
using Xunity.ScriptableReferences;

namespace Xunity.Behaviours
{
    public class ShaderGlobalVectorSetter : GameBehaviour
    {
        [SerializeField] StringReference variableName;

        void Update()
        {
            Shader.SetGlobalVector(variableName, transform.position);
        }
    }
}