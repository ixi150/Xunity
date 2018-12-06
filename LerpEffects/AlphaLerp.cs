using UnityEngine;

namespace Xunity.LerpEffects
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class AlphaLerp : LerpEffect
    {
        [SerializeField] RangedFloat alphaRange;

        SpriteRenderer alphaTarget;

        public override void Refresh()
        {
            GetComponentIfNull(ref alphaTarget);
            var color = alphaTarget.color;
            color.a = Mathf.Lerp(alphaRange.minValue, alphaRange.maxValue, Lerp);
            alphaTarget.color = color;
        }
    }
}