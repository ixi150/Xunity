using UnityEditor;
using UnityEngine;

namespace Xunity.Editor.BuiltInEditors
{
    //[CustomEditor(typeof(Transform), true)]
    public class TransformEditor : DecoratorEditor
    {
        public TransformEditor() : base("TransformInspector")
        {
        }
    }
}
