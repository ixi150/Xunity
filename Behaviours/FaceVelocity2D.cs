using UnityEngine;
using Xunity.ScriptableReferences;

namespace Xunity.Behaviours
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class FaceVelocity2D : GameBehaviour
    {
        [SerializeField] FloatReference smoothTime = FloatReference.New(true, .5f);

        Rigidbody2D rb;
        Vector2 smoothVelocity;

        protected override void Awake()
        {
            base.Awake();

            GetComponentIfNull(ref rb);
        }

        void FixedUpdate()
        {
            //Mathf.SmoothDampAngle()
        
            transform.right = Vector2.SmoothDamp(transform.right, rb.velocity, ref smoothVelocity, smoothTime);
        }
    }
}