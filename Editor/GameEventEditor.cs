using UnityEditor;
using UnityEngine;
using Xunity.ScriptableEvents;

namespace Xunity.Editor
{
    [CustomEditor(typeof(GameEvent))]
    public class EventEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUI.enabled = Application.isPlaying;

            var e = target as GameEvent;
            if (GUILayout.Button("Raise"))
                e.Raise();
            
            if (!Application.isPlaying)
                return;
            
            GUI.enabled = false;
            EditorGUILayout.LabelField("Current","Listeners");
            var type = typeof(GameEventListener);
            var i = 0;
            foreach (var listener in e.Listeners)
                EditorGUILayout.ObjectField("" + i++, listener, type, true);
        }
    }
}