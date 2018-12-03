using UnityEditor;
using UnityEngine;
using Xunity.ScriptableVariables;

namespace Xunity.Editor.Drawers
{
    [CustomPropertyDrawer(typeof(ScriptableVariableBase), true)]
    public class ScriptableVariableDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // Using BeginProperty / EndProperty on the parent property means that
            // prefab override logic works on the entire property.
            EditorGUI.BeginProperty(position, label, property);

            // Draw label
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            // Don't make child fields be indented
            int indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            // Calculate rects
            var refRect = position;
            refRect.width *= .666f;
            var valueRect = position;
            valueRect.x += refRect.width;
            valueRect.width -= refRect.width;

            // Draw fields - pass GUIContent.none to each so they are drawn without labels
            EditorGUI.PropertyField(refRect, property, GUIContent.none);
            if (property.objectReferenceValue)
            {
                EditorGUI.LabelField(valueRect, property.objectReferenceValue.ToString());
            }
            else if (GUI.Button(valueRect, "Create"))
            {
                var typeName = property.type.Replace("PPtr<$", "").Trim('>');
                var newSo = ScriptableObject.CreateInstance(typeName);
                AssetDatabase.CreateAsset(newSo, "");
                property.objectReferenceValue = newSo;
            }

            // Set indent back to what it was
            EditorGUI.indentLevel = indent;

            EditorGUI.EndProperty();
        }
    }
}