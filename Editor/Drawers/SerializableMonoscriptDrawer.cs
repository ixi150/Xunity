using UnityEditor;
using UnityEngine;
using Xunity.Authorization;
using Xunity.ScriptableVariables;

namespace Xunity.Editor.Drawers
{
    [CustomPropertyDrawer(typeof(SerializableMonoscript))]
    public class SerializableMonoscriptDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginChangeCheck();

            var monoProperty = property.FindPropertyRelative("monoScript");
            EditorGUI.PropertyField(position, monoProperty, GUIContent.none);

            if (!EditorGUI.EndChangeCheck())
                return;

            Undo.RecordObject(property.serializedObject.targetObject, "Changed SerializableMonoscript");
            var mono = monoProperty.objectReferenceValue as MonoScript;
            property.FindPropertyRelative("name").stringValue = SerializableMonoscript.MonoToString(mono);
            property.serializedObject.ApplyModifiedProperties();
        }
    }
}