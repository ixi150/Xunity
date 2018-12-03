using UnityEditor;
using UnityEngine;
using Xunity.Sets;

namespace Xunity.Editor
{
    [CustomEditor(typeof(ScriptableSet), true)]
    public class ScriptableSetEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUI.enabled = Application.isPlaying;

            var e = target as ScriptableSet;

            if (!Application.isPlaying || e == null)
                return;

            GUI.enabled = false;
            EditorGUILayout.LabelField("Current", "Members");
            var type = e.Type;
            var i = 0;
            foreach (var o in e.Objects)
                EditorGUILayout.ObjectField("" + i++, o, type, true);
        }
    }
}