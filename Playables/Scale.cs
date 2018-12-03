using UnityEngine;

namespace Xunity.Playables
{
    public class Scale : Playable
    {
        [SerializeField] AnimationCurve scalingCurve = new AnimationCurve();
        
        Vector3 initialScale;
        float maxTime;
        
        protected override void Awake()
        {
            base.Awake();
            
            initialScale = transform.localScale;
        }
        
        protected override void OnFinishPlaying()
        {
        }

        protected override void OnStartPlaying()
        {
        }

        protected override void OnPlayUpdate(float progress)
        {
            transform.localScale = scalingCurve.Evaluate(progress) * initialScale;
        }

        protected override void OnStoppedPlaying()
        {
        }
    }
}