using UnityEditor;
using UnityEngine;
using Xunity.Authorization;
using Xunity.ScriptableVariables;

namespace Xunity.Editor.Drawers
{
    [CustomPropertyDrawer(typeof(AuthorizedSources))]
    public class AuthorizedSourcesDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            float height = base.GetPropertyHeight(property, label);
            var isRestricted = property.FindPropertyRelative("restrictAccess");
            if (!isRestricted.boolValue)
                return height;

            var sources = property.FindPropertyRelative("authorizedSources");
            return height * (sources.arraySize + 1);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var isRestricted = property.FindPropertyRelative("restrictAccess");

            var rectRestricted = new Rect(position) {height = base.GetPropertyHeight(property, label)};
            EditorGUI.PropertyField(rectRestricted, isRestricted);

            if (!isRestricted.boolValue)
                return;

            var sources = property.FindPropertyRelative("authorizedSources");
            var rectSources = new Rect(position);
            rectSources.yMin += rectRestricted.height;
            EditorGUI.PropertyField(rectSources, sources);
        }
    }
}