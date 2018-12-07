using UnityEngine;
using Xunity.ScriptableReferences;

namespace Xunity.Behaviours
{
    [ExecuteInEditMode]
    public class MustBeChildOf : GameBehaviour
    {
        const string PREFIX = ">>>";
        const string SUFIX = "<<<";

        [SerializeField] StringReference parentName;

        string GoodParentName
        {
            get { return PREFIX + parentName + SUFIX; }
        }

        protected override void Awake()
        {
            if (gameObject.scene.name == null
                || parentName == default(string)
                || parentName == null
                || parentName == ""
                || IsGoodParent())
                return;

            SetGoodParent();
        }

        void SetGoodParent()
        {
            var parent = GameObject.Find(GoodParentName);
            if (parent == null)
            {
                parent = new GameObject(GoodParentName);
            }

            transform.SetParent(parent.transform);
        }

        bool IsGoodParent()
        {
            return IsGoodParent(transform.parent);
        }

        bool IsGoodParent(Transform parent)
        {
            if (parent == null)
                return false;
            return parent.name == GoodParentName || IsGoodParent(parent.parent);
        }
    }
}