using UnityEngine;

namespace Xunity.Behaviours
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class RotationFromVelocity2D : GameBehaviour
    {
        [SerializeField] AnimationCurve yToRotation;

        Rigidbody2D rb;

        protected override void Awake()
        {
            base.Awake();
            GetComponentIfNull(ref rb);
        }

        void Update()
        {
            transform.eulerAngles = new Vector3(0, 0, yToRotation.Evaluate(rb.velocity.y));
        }
    }
}