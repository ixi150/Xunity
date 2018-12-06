using UnityEditor;
using Xunity.ScriptableVariables;

namespace Xunity.Editor
{
    [CustomEditor(typeof(RangedFloatVariable))]
    public class RangedFloatVariableEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            //base.OnInspectorGUI();
            
            var val = serializedObject.FindProperty("value");
            var min = serializedObject.FindProperty("min");
            var max = serializedObject.FindProperty("max");
            var sources = serializedObject.FindProperty("authorizedSources");
            
            EditorGUI.BeginChangeCheck();
            float value = EditorGUILayout.Slider("Value", val.floatValue, min.floatValue, max.floatValue);
            if (value != val.floatValue)
            {
                val.floatValue = value;
                serializedObject.ApplyModifiedProperties();
            }

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(min);
            EditorGUILayout.PropertyField(max);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.PropertyField(sources);
            
            if (EditorGUI.EndChangeCheck())
                serializedObject.ApplyModifiedProperties();
        }
    }
}