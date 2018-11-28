using System;
using UnityEngine;
using Xunity.Exceptions;

namespace Xunity.Behaviours
{
    public abstract class GameBehaviour : MonoBehaviour
    {
        protected static readonly YieldInstruction waitForEndOfFrame = new WaitForEndOfFrame();
        protected static readonly YieldInstruction waitForFixedUpdate = new WaitForFixedUpdate();

        Transform cachedTransform;
        GameObject cachedGameObject;

        // ReSharper disable once InconsistentNaming
        // ReSharper disable once MemberCanBeProtected.Global
        // ReSharper disable once MemberCanBePrivate.Global
        public new Transform transform
        {
#if UNITY_EDITOR
            get { return cachedTransform ? cachedTransform : cachedTransform = base.transform; }
#else
            get { return caughtTransform; }
#endif
        }

        // ReSharper disable once InconsistentNaming
        // ReSharper disable once MemberCanBeProtected.Global
        // ReSharper disable once MemberCanBePrivate.Global
        public new GameObject gameObject
        {
#if UNITY_EDITOR
            get { return cachedGameObject ? cachedGameObject : cachedGameObject = base.gameObject; }
#else
            get { return caughtGameObject; }
#endif
        }

        public Vector3 Position
        {
            get { return transform.position; }
            set { transform.position = value; }
        }

        public Vector3 LocalPosition
        {
            get { return transform.localPosition; }
            set { transform.localPosition = value; }
        }

        public Vector3 Forward
        {
            get { return transform.forward; }
            set { transform.forward = value; }
        }

        public void Activate()
        {
            gameObject.SetActive(true);
        }

        public void Activate(float delay)
        {
            Invoke(Activate, delay);
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
            CancelInvoke();
            StopAllCoroutines();
        }

        public void Deactivate(float delay)
        {
            Invoke(Deactivate, delay);
        }

        protected static void EmptyAction()
        {
        }

        protected virtual void Awake()
        {
            cachedTransform = base.transform;
            cachedGameObject = base.gameObject;
        }

        protected void ForEachChild(Action<Transform> actionOnChild)
        {
            foreach (Transform child in transform)
                actionOnChild(child);
        }

        protected new void Invoke(string action, float delay)
        {
            throw new WrongMethodException("Use Invoke(Action action, float delay) instead");
        }

        protected new void InvokeRepeating(string action, float delay, float repeatRate)
        {
            throw new WrongMethodException("Use Invoke(Action action, float delay, float repeatRate) instead");
        }

        protected void Invoke(Action action, float delay)
        {
            base.Invoke(action.Method.Name, delay);
        }

        protected void Invoke(Action action, float delay, float repeatRate)
        {
            base.InvokeRepeating(action.Method.Name, delay, repeatRate);
        }

        protected void GetComponent<T>(out T component) where T : Component
        {
            component = GetComponent<T>();
        }

        protected void GetComponentIfNull<T>(ref T component) where T : Component
        {
            if (component == null)
                GetComponent(out component);
        }
    }
}