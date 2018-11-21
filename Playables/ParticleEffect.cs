using UnityEngine;
using Xunity.Extensions;

namespace Xunity.Playables
{
    public class ParticleEffect : Playable
    {
        [SerializeField] ParticleSystem effectPrefab;
        [SerializeField] ParticleEffectMethod effectGetMethod;
        [SerializeField] Vector3 particleOffset;

        ParticleSystem currentEffect;

        public enum ParticleEffectMethod
        {
            ByObjectPooling = 0,
            SpawnChildEffect = 1,
        }

        public Vector3 EffectWorldPosition
        {
            get { return transform.TransformPoint(particleOffset); }
        }

        protected override void OnStartPlaying()
        {
            currentEffect = PlayParticleEffect(effectPrefab, EffectWorldPosition, transform.rotation);
        }

        protected override void OnPlayUpdate(float progress)
        {
        }

        protected override void OnStoppedPlaying()
        {
            currentEffect.Deactivate();
        }

        protected override void OnFinishPlaying()
        {
            currentEffect.Deactivate();
        }

        ParticleSystem PlayParticleEffect(ParticleSystem particlePrefab, Vector3 location)
        {
            return PlayParticleEffect(particlePrefab, location, Quaternion.identity);
        }

        ParticleSystem PlayParticleEffect(ParticleSystem particlePrefab, Vector3 location, Quaternion rotation)
        {
            var instance = GetParticleInstance();
            var instanceTransform = instance.transform;
            instanceTransform.position = location;
            instanceTransform.rotation = rotation;
            instance.Play();
            return instance;
        }

        ParticleSystem GetParticleInstance()
        {
            switch (effectGetMethod)
            {
                case ParticleEffectMethod.ByObjectPooling:
                    return null; //todo: implement object pooler
                case ParticleEffectMethod.SpawnChildEffect:
                    if (currentEffect == null)
                        currentEffect = Instantiate(effectPrefab, transform);
                    currentEffect.Activate();
                    return currentEffect;
                default:
                    Debug.LogError("Unknown ParticleEffectMethod: " + effectGetMethod);
                    return null;
            }
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(Position, EffectWorldPosition);
        }
    }
}