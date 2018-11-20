using UnityEngine;

namespace Orbia.Utils
{
    public class RotateParticlesFacingTarget : MonoBehaviour
    {
        [SerializeField] float _offset;

        ParticleSystem ps;

        public Transform Target { get; set; }

        Transform CatchedTransform { get; set; }

        void Awake()
        {
            ps = GetComponent<ParticleSystem>();
            CatchedTransform = transform;
        }

        void Update()
        {
            if (Target == null) 
                return;
            
            var main = ps.main;
            var startRotation = main.startRotation;
            float look = -Vector2.SignedAngle(Vector2.up, Target.position - CatchedTransform.position);
            startRotation.constant = (_offset + look) * Mathf.PI / 180f;
            main.startRotation = startRotation;
        }
    }
}