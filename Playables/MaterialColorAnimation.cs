using UnityEngine;

namespace IXI
{
    public class MaterialColorAnimation : Playable
    {
        [Header("MaterialColorAnimation")]
        [SerializeField] new Renderer renderer;
        [SerializeField] Gradient missColorGradient;

        Material material;
        Material sharedMaterial;
        Color initialColor;
        GradientColorKey[] gradientColorKeys;

        protected override void Awake()
        {
            base.Awake();

            sharedMaterial = renderer.sharedMaterial;
            initialColor = sharedMaterial.color;
            gradientColorKeys = missColorGradient.colorKeys;
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            gradientColorKeys[0].color = initialColor;
            missColorGradient.colorKeys = gradientColorKeys;
        }

        void OnDisable()
        {
            renderer.material = sharedMaterial;
        }

        protected override void OnStartPlaying()
        {
            material = renderer.material;
        }

        protected override void OnPlayUpdate(float progress)
        {
            material.color = missColorGradient.Evaluate(progress);
        }
    }
}