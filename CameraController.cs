using IXI.ReferenceVariables;
using IXI.Utils;
using UnityEngine;

namespace Xunity
{
    public class CameraController : GameBehaviour
    {
        [SerializeField] Transform target;
        [SerializeField] Vector3Reference targetOffset = Vector3Reference.New(useConstant: true, value: Vector3.back);
        [SerializeField] FloatReference smoothTime = FloatReference.New(useConstant: true, value: .5f);

        new protected Camera camera;
        
        Vector3 velocity;
        Vector3 targetPosition;

        public Transform Target { get { return target; } }

        protected override void Awake()
        {
            base.Awake();

            camera = GetComponent<Camera>();
        }

        void LateUpdate()
        {
            if (target)
                targetPosition = target.position;

            Position = Vector3.SmoothDamp(Position, targetPosition + targetOffset, ref velocity, smoothTime);
        }
    }
}