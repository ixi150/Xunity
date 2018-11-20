using UnityEngine;

namespace Orbia.Utils
{
    public class ScalingBehaviour : MonoBehaviour
    {
        [SerializeField] AnimationCurve _scalingCurve = new AnimationCurve();
        
        Vector3 initialScale;
        float maxTime;

        Transform CatchedTransform { get; set; }

        void Awake()
        {
            CatchedTransform = transform;
            initialScale = CatchedTransform.localScale;
            maxTime = _scalingCurve.keys[_scalingCurve.length - 1].time;
        }

        void Update()
        {
            CatchedTransform.localScale = initialScale * _scalingCurve.Evaluate(Time.time % maxTime);
        }
    }
}