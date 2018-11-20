using UnityEngine;

namespace Xunity.Extensions
{
    public static class TransformExtensions
    {
        public static void ModifyPosition(this Transform transform, float? x = null, float? y = null, float? z = null)
        {
            transform.position = transform.position.Modified(x, y, z);
        }

        public static void ModifyLocalPosition(this Transform transform, float? x = null, float? y = null, float? z = null)
        {
            transform.localPosition = transform.localPosition.Modified(x, y, z);
        }

        public static void ModifyLocalScale(this Transform transform, float? x = null, float? y = null, float? z = null)
        {
            transform.localScale = transform.localScale.Modified(x, y, z);
        }

        public static void ResetAll(this Transform transform)
        {
            transform.ResetPosition();
            transform.ResetRotation();
            transform.ResetScale();
        }

        public static void ResetPosition(this Transform transform)
        {
            transform.localPosition = Vector3.zero;
        }

        public static void ResetRotation(this Transform transform)
        {
            transform.localRotation = Quaternion.identity;
        }

        public static void ResetScale(this Transform transform)
        {
            transform.localScale = Vector3.one;
        }

        public static Transform GetFirstChild(this Transform t)
        {
            return t.GetChild(0);
        }
        
        public static Transform GetLastChild(this Transform t)
        {
            return t.GetChild(t.childCount - 1);
        }
    }
}