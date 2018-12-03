using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using Xunity.Extensions;
using Xunity.ScriptableFunctions;
using Xunity.ScriptableReferences;
using Xunity.Sets;

namespace Xunity.Behaviours
{
    public class CameraController : GameBehaviour
    {
        [SerializeField] SetCollection targets;
        [SerializeField] Vector3Reference targetOffset = Vector3Reference.New(useConstant: true, value: Vector3.back);
        [SerializeField] FloatReference smoothTime = FloatReference.New(useConstant: true, value: .5f);
        [SerializeField] OpaqueSortMode sortModeOpaque = OpaqueSortMode.Default;
        [SerializeField] TransparencySortMode sortModeTransparency = TransparencySortMode.Default;
        [SerializeField] Vector3FromComponent[] offsetModifiers;

        protected new Camera camera;
        Vector3 velocity;
        Vector3 targetPosition;

        public IEnumerable<Transform> Targets
        {
            get { return targets ? targets.Items : null; }
        }

        public virtual Vector3 TargetPosition
        {
            get { return TargetsAveragePosition; }
        }

        protected virtual Vector3 TargetOffset
        {
            get { return targetOffset + ModifiersOffset; }
        }

        protected virtual Vector3 ModifiersOffset
        {
            get
            {
                return offsetModifiers == null || targets == null || offsetModifiers.Length == 0 || targets.Count == 0
                    ? Vector3.zero
                    : Targets.Select(GetModifiersOffsetFromTarget)
                          .Average() / targets.Count;
            }
        }

        protected virtual Vector3 TargetsAveragePosition
        {
            get
            {
                return targets.Items
                    .Select(GetTargetsPosition)
                    .Average();
            }
        }

        protected virtual Vector3 GetTargetsPosition(Transform target)
        {
            return target.position;
        }

        protected override void Awake()
        {
            base.Awake();

            camera = GetComponent<Camera>();
            camera.opaqueSortMode = sortModeOpaque;
            camera.transparencySortMode = sortModeTransparency;
        }

        protected virtual void FixedUpdate()
        {
            if (targets && targets.Count > 0)
                targetPosition = TargetPosition;

            Position = Vector3.SmoothDamp(Position, TargetOffset, ref velocity, smoothTime);
        }

        Vector3 GetModifiersOffsetFromTarget(Transform target)
        {
            return offsetModifiers
                       .NotNull()
                       .Select(modifier => modifier.Invoke(target))
                       .Sum();
        }
    }
}