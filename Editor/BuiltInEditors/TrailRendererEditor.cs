using System;
using System.Reflection;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace Xunity.Editor.BuiltInEditors
{
    [CustomEditor(typeof(TrailRenderer), true), CanEditMultipleObjects]
    public class TrailRendererEditor : DecoratorEditor
    {
        const string scriptName = "TrailRendererInspector";
        public TrailRendererEditor() : base(scriptName) { }

        SerializedProperty layerID, order;
        string[] sortingLayerNames;
        private void OnEnable()
        {
            //Init(scriptName);
            layerID = serializedObject.FindProperty("m_SortingLayerID");
            order = serializedObject.FindProperty("m_SortingOrder");
            sortingLayerNames = GetSortingLayerNames();
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.PropertyField(order);
            int index = layerID.intValue;
            index = EditorGUILayout.Popup("Sorting Layer", index, sortingLayerNames);
            if (GUI.changed)
            {

                if (layerID.intValue != index)
                {
                    //layerID.intValue = index;
                    foreach (var g in Selection.gameObjects)
                    {
                        var trail = g.GetComponent<TrailRenderer>();
                        if (trail)
                        {
                            trail.sortingOrder = index;
                            EditorUtility.SetDirty(trail);
                        }
                    }
                }

                serializedObject.ApplyModifiedProperties();
            }

            base.OnInspectorGUI();
        }

        public string[] GetSortingLayerNames()
        {
            Type internalEditorUtilityType = typeof(InternalEditorUtility);
            PropertyInfo sortingLayersProperty = internalEditorUtilityType.GetProperty("sortingLayerNames", BindingFlags.Static | BindingFlags.NonPublic);
            var sortingLayers = (string[])sortingLayersProperty.GetValue(null, new object[0]);
            return sortingLayers;
        }
    }
}
