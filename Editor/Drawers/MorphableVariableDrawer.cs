using UnityEditor;
using UnityEngine;
using Xunity.Morphables;
using Xunity.ScriptableReferences;

namespace Xunity.Editor.Drawers
{
    [CustomPropertyDrawer(typeof(MorphableBase), true)]
    public class MorphableVariableDrawer : PropertyDrawer
    {
        /// <summary>
        /// Options to display in the popup to select constant or variable.
        /// </summary>
        readonly string[] popupOptions = {"Variable", "Array"};

        /// <summary> Cached style to use to draw the popup button. </summary>
        GUIStyle popupStyle;
        GUIStyle plusStyle;
        GUIStyle minusStyle;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            float baseHeight = base.GetPropertyHeight(property, label);
            var isArray = property.FindPropertyRelative("isArray");
            int multiplier = isArray.boolValue
                ? property.FindPropertyRelative("array").arraySize + 1
                : 1;
            return baseHeight * multiplier;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            CashGuiStyle(ref popupStyle, "PaneOptions");
            CashGuiStyle(ref plusStyle, "OL Plus");
            CashGuiStyle(ref minusStyle, "OL Minus");
            var iconWidth = popupStyle.fixedWidth + popupStyle.margin.right;
            
            EditorGUI.BeginChangeCheck();
            label = EditorGUI.BeginProperty(position, label, property);
            position = EditorGUI.PrefixLabel(position, label);

            // Calculate rect for configuration button
            var buttonRect = new Rect(position);
            buttonRect.yMin += popupStyle.margin.top;
            buttonRect.width = iconWidth;
            buttonRect.height = popupStyle.fixedHeight;
            position.xMin = buttonRect.xMax;
            //buttonRect.x -= buttonRect.width;

            // Store old indent level and set it to 0, the PrefixLabel takes care of it
            int indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            // Get properties
            EditorGUI.BeginChangeCheck();
            var isArray = property.FindPropertyRelative("isArray");
            int popupIndex = isArray.boolValue ? 1 : 0;
            popupIndex = EditorGUI.Popup(buttonRect, popupIndex, popupOptions, popupStyle);
            isArray.boolValue = popupIndex > 0;

            var array = property.FindPropertyRelative("array");
            if (isArray.boolValue)
            {
                var leftRect = new Rect(position);
                leftRect.height /= array.arraySize + 1;
                var rightRect = new Rect(leftRect);
                leftRect.xMax -= iconWidth;
                rightRect.xMin = leftRect.xMax;

                array.arraySize = EditorGUI.DelayedIntField(leftRect, "Size", array.arraySize);
                leftRect.xMin -= buttonRect.width;
                if (EditorGUI.Toggle(rightRect, "", false, plusStyle))
                    Debug.Log("Add");

                for (var i = 0; i < array.arraySize; i++)
                {
                    rightRect.y = leftRect.y += leftRect.height;
                    EditorGUI.PropertyField(leftRect, array.GetArrayElementAtIndex(i), GUIContent.none);
                    if (EditorGUI.Toggle(rightRect, "", false, minusStyle))
                        Debug.Log("Remove " + i);
                }
            }
            else
            {
                EditorGUI.PropertyField(position, array.GetArrayElementAtIndex(0), GUIContent.none);
            }

            if (EditorGUI.EndChangeCheck())
                property.serializedObject.ApplyModifiedProperties();

            EditorGUI.indentLevel = indent;
            EditorGUI.EndProperty();
        }

        static void CashGuiStyle(ref GUIStyle style, string name)
        {
            if (style == null)
                style = new GUIStyle(GUI.skin.GetStyle(name))
                    {imagePosition = ImagePosition.ImageOnly};
        }
    }
}