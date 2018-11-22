using UnityEngine;
using Xunity.ScriptableReferences;

namespace Xunity.Behaviours
{
    public class CameraController : GameBehaviour
    {
        [SerializeField] Transform target;
        [SerializeField] Vector3VariableReference targetOffset = Vector3VariableReference.New(useConstant: true, value: Vector3.back);
        [SerializeField] FloatVariableReference smoothTime = FloatVariableReference.New(useConstant: true, value: .5f);

        new protected Camera camera;
        Vector3 velocity;
        Vector3 targetPosition;

        public Transform Target
        {
            get { return target; }
        }

        protected virtual Vector3 TargetOffset
        {
            get { return targetOffset; }
        }

        protected override void Awake()
        {
            base.Awake();

            camera = GetComponent<Camera>();
        }

        protected virtual void LateUpdate()
        {
            if (target)
                targetPosition = target.position;

            Position = Vector3.SmoothDamp(Position, targetPosition + TargetOffset, ref velocity, smoothTime);
        }
    }
}