using UnityEditor;
using UnityEngine;

namespace Xunity.Editor.Drawers
{
    [CustomPropertyDrawer(typeof(IntReference))]
    [CustomPropertyDrawer(typeof(FloatReference))]
    [CustomPropertyDrawer(typeof(BoolReference))]
    [CustomPropertyDrawer(typeof(StringReference))]
    [CustomPropertyDrawer(typeof(Vector3Reference))]
    public class ReferenceVariableDrawer : PropertyDrawer
    {
        enum VariableSource
        {
            UseReference = 0,
            UseConstant = 1,
        }

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
            var boolRect = position;
            boolRect.width = 16f;
            var valueRect = position;
            valueRect.x += boolRect.width;
            valueRect.width -= boolRect.width;

            // Draw fields - passs GUIContent.none to each so they are drawn without labels
            EditorGUI.BeginChangeCheck();
            var useConstant = property.FindPropertyRelative("useConstant");
            var varSource = useConstant.boolValue ? VariableSource.UseConstant : VariableSource.UseReference;
            varSource = (VariableSource) EditorGUI.EnumPopup(boolRect, varSource);
            useConstant.boolValue = varSource == VariableSource.UseConstant;

            var varProp = property.FindPropertyRelative(useConstant.boolValue ? "value" : "reference");
            EditorGUI.PropertyField(valueRect, varProp, GUIContent.none);

            // Set indent back to what it was
            EditorGUI.indentLevel = indent;
            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(property.serializedObject.targetObject);
            }

            EditorGUI.EndProperty();
        }
    }
}