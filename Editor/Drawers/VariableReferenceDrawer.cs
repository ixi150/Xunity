using UnityEditor;
using UnityEngine;
using Xunity.ScriptableReferences;

namespace Xunity.Editor.Drawers
{
    [CustomPropertyDrawer(typeof(VariableReferenceBase), true)]
    public class VariableReferenceDrawer : PropertyDrawer
    {
        /// <summary>
        /// Options to display in the popup to select constant or variable.
        /// </summary>
        readonly string[] popupOptions = {"Use Variable", "Use Constant"};

        /// <summary> Cached style to use to draw the popup button. </summary>
        GUIStyle popupStyle;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (popupStyle == null)
                popupStyle = new GUIStyle(GUI.skin.GetStyle("PaneOptions"))
                    {imagePosition = ImagePosition.ImageOnly};

            EditorGUI.BeginChangeCheck();
            label = EditorGUI.BeginProperty(position, label, property);
            position = EditorGUI.PrefixLabel(position, label);

            // Calculate rect for configuration button
            var buttonRect = new Rect(position);
            buttonRect.yMin += popupStyle.margin.top;
            buttonRect.width = popupStyle.fixedWidth + popupStyle.margin.right;
            buttonRect.x -= buttonRect.width;
            //position.xMin = buttonRect.xMax;

            // Store old indent level and set it to 0, the PrefixLabel takes care of it
            int indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            // Get properties
            EditorGUI.BeginChangeCheck();
            var useConstant = property.FindPropertyRelative("useConstant");
            useConstant.boolValue =
                EditorGUI.Popup(buttonRect, useConstant.boolValue ? 1 : 0, popupOptions, popupStyle) > 0;
            var varProp = property.FindPropertyRelative(useConstant.boolValue ? "value" : "reference");
            EditorGUI.PropertyField(position, varProp, GUIContent.none);

            if (EditorGUI.EndChangeCheck())
                property.serializedObject.ApplyModifiedProperties();

            EditorGUI.indentLevel = indent;
            EditorGUI.EndProperty();
        }
    }
}