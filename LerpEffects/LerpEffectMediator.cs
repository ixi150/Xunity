using System.Collections;
using System.Linq;
using UnityEngine;
using Xunity.Behaviours;
using Xunity.Extensions;
using Xunity.Morphables;
using Xunity.ScriptableReferences;
using Xunity.ScriptableVariables;

namespace Xunity.LerpEffects
{
    public class LerpEffectMediator : GameBehaviour
    {
        [SerializeField] FloatVariable value;
        [SerializeField] LerpEffectMorph lerpEffects;
        [SerializeField] AnimationCurve easeCurve = AnimationCurve.Linear(0, 0, 1, 1);
        [SerializeField] FloatReference duration;

        void OnEnable()
        {
            value.ValueChanged += OnChanged;
            SetValue(value);
        }

        void OnDisable()
        {
            value.ValueChanged -= OnChanged;
        }

        void OnChanged(float v)
        {
            StopAllCoroutines();
            float originalValue = lerpEffects.First().Lerp;
            if (originalValue > v)
                SetValue(v);
            else
                StartCoroutine(EaseValue(originalValue));
        }

        void SetValue(float v)
        {
            foreach (var lerpEffect in lerpEffects.NotNull())
            {
                lerpEffect.Lerp = v;
            }
        }

        IEnumerator EaseValue(float originalValue)
        {
            var elapsed = 0f;
            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                float ease = easeCurve.Evaluate(elapsed / duration);
                float lerpedValue = Mathf.LerpUnclamped(originalValue, value, ease);
                SetValue(lerpedValue);
                yield return waitForEndOfFrame;
            }
        }
    }
}