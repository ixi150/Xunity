using UnityEngine;
using Xunity.ScriptableReferences;

namespace Xunity.Playables
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayableForce2D : Playable
    {
        [SerializeField] Vector2Reference startForce;
        [SerializeField] new Vector2Reference constantForce;
        [SerializeField] Vector2Reference limit;

        Rigidbody2D rb;

        protected override void Awake()
        {
            base.Awake();
            rb = GetComponent<Rigidbody2D>();
        }

        protected override void OnStartPlaying()
        {
            AddForce(startForce.Value, ForceMode2D.Impulse);
        }

        protected override void OnPlayUpdate(float progress)
        {
            AddForce(constantForce.Value, ForceMode2D.Force);
        }

        protected override void OnStoppedPlaying()
        {
        }

        protected override void OnFinishPlaying()
        {
        }

        void AddForce(Vector2 force, ForceMode2D forceMode)
        {
            if (rb.velocity.x > limit.Value.x) force.x = 0;
            if (rb.velocity.y > limit.Value.y) force.y = 0;
            rb.AddForce(force, forceMode);
        }
    }
}