using UnityEditor;
using UnityEngine;
using Xunity.ScriptableVariables;

namespace Xunity.Editor.Drawers
{
    [CustomPropertyDrawer(typeof(IntVariable))]
    [CustomPropertyDrawer(typeof(FloatVariable))]
    [CustomPropertyDrawer(typeof(BoolVariable))]
    [CustomPropertyDrawer(typeof(StringVariable))]
    [CustomPropertyDrawer(typeof(Vector3Variable))]
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

            // Draw fields - passs GUIContent.none to each so they are drawn without labels
            EditorGUI.PropertyField(refRect, property, GUIContent.none);
            if (property.objectReferenceValue)
            {
                EditorGUI.LabelField(valueRect, property.objectReferenceValue.ToString());
            }

            // Set indent back to what it was
            EditorGUI.indentLevel = indent;

            EditorGUI.EndProperty();
        }
    }
}