using UnityEngine;

namespace Xunity.LerpEffects
{
    [ExecuteInEditMode]
    public class ScaleLerp : LerpEffect
    {
        [SerializeField] RangedFloat scaleRange;
        [SerializeField] float scale = 1;

        public override void Refresh()
        {
            transform.localScale =
                Vector3.one * scale * Mathf.Lerp(scaleRange.minValue, scaleRange.maxValue, Lerp);
        }
    }
}