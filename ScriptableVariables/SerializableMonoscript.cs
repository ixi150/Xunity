using System;
using UnityEngine;

namespace Xunity.ScriptableVariables
{
    [Serializable]
    public class SerializableMonoscript : ISerializationCallbackReceiver
    {
        [SerializeField] string name;

#if UNITY_EDITOR
        [SerializeField] UnityEditor.MonoScript monoScript;
#endif

        public static implicit operator string(SerializableMonoscript serializableMonoscript)
        {
            return serializableMonoscript.name;
        }

#if UNITY_EDITOR
        public static string MonoToString(UnityEditor.MonoScript mono)
        {
            return mono == null ? "" : TypeToString(mono.GetClass());
        }
#endif

        public static string TypeToString(Type type)
        {
            return type == null ? "" : type.ToString();
        }

        public void OnAfterDeserialize() { }

        public void OnBeforeSerialize()
        {
#if UNITY_EDITOR
            name = MonoToString(monoScript);
#endif
        }
    }
}