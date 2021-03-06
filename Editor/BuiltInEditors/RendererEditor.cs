﻿using System;
using System.Reflection;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace Xunity.Editor.BuiltInEditors
{
    [CustomEditor(typeof(Renderer), true)]
    public class SortingLayerEditor : UnityEditor.Editor
    {

        SerializedProperty[] properties;
        string[] sortingLayerNames;

        void OnEnable()
        {
            //if (Attribute.IsDefined(target.GetType(), typeof(HasSortingLayerName)))
            //{
            //    var sortingLayer = (HasSortingLayerName)Attribute.GetCustomAttribute(target.GetType(), typeof(HasSortingLayerName));
            //    properties = sortingLayer.Names.Select(s =>
            //    {
            //        return serializedObject.FindProperty(s);
            //    }).ToArray();
            //    sortingLayerNames = GetSortingLayerNames();
            //}
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (properties != null && sortingLayerNames != null)
            {
                foreach (var p in properties)
                {
                    if (p == null)
                    {
                        continue;
                    }
                    int index = Mathf.Max(0, Array.IndexOf(sortingLayerNames, p.stringValue));
                    index = EditorGUILayout.Popup(p.displayName, index, sortingLayerNames);

                    p.stringValue = sortingLayerNames[index];
                }

                if (GUI.changed)
                {
                    serializedObject.ApplyModifiedProperties();
                }
            }
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