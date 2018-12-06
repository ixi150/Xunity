using UnityEngine;

namespace Xunity.Playables
{
    public class Scale : Playable
    {
        [SerializeField] AnimationCurve scalingCurve = AnimationCurve.Linear(0,1,1,1);
        
        Vector3 initialScale;
        
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