using UnityEngine;
using Xunity.Extensions;
using Xunity.Morphables;

namespace Xunity.LerpEffects
{
    public class LerpEffectsController : LerpEffect
    {
        [SerializeField] LerpEffectMorph effects;

        public override void Refresh()
        {
            foreach (var effect in effects.NotNull())
            {
                effect.Lerp = Lerp;
            }
        }
    }
}
