using System;
using UnityEngine;

namespace Xunity
{
    public abstract class GameBehaviour : MonoBehaviour
    {
        Transform catchedTransform;
        GameObject catchedGameObject;

        public Transform CatchedTransform
        {
#if UNITY_EDITOR
            get { return catchedTransform ? catchedTransform : catchedTransform = transform; }
#else
            get { return catchedTransform; }
#endif
        }

        public GameObject CatchedGameObject
        {
#if UNITY_EDITOR
            get { return catchedGameObject ? catchedGameObject : catchedGameObject = gameObject; }
#else
            get { return catchedGameObject; }
#endif
        }

        public Vector3 Position
        {
            get { return CatchedTransform.localPosition; }
            set { CatchedTransform.localPosition = value; }
        }

        public Vector3 Forward
        {
            get { return CatchedTransform.forward; }
            set { CatchedTransform.forward = value; }
        }

        public void Activate()
        {
            CatchedGameObject.SetActive(true);
        }

        public void Activate(float delay)
        {
            Invoke("Activate", delay);
        }

        public void Deactivate()
        {
            CatchedGameObject.SetActive(false);
            CancelInvoke();
            StopAllCoroutines();
        }

        public void Deactivate(float delay)
        {
            Invoke("Deactivate", delay);
        }

        protected virtual void Awake()
        {
            catchedTransform = transform;
            catchedGameObject = gameObject;
        }

        protected void ForEachChild(Action<Transform> actionOnChild)
        {
            foreach (Transform child in CatchedTransform)
                actionOnChild(child);
        }
    }
}