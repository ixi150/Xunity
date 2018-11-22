using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Transform), true)]
public class TransformEditor : DecoratorEditor
{
    public TransformEditor() : base("TransformInspector")
    {
    }

    AnimationCurve curve = new AnimationCurve();
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        curve = EditorGUILayout.CurveField(curve);
    }
}
