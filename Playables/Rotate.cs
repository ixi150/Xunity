using UnityEngine;
using Xunity.ScriptableReferences;

namespace Xunity.Playables
{
    public class Rotate : Playable
    {
        [SerializeField] Vector3Reference rotationSpeed = Vector3Reference.New(true);

        protected override void OnStartPlaying()
        {
        }

        protected override void OnStoppedPlaying()
        {
        }

        protected override void OnFinishPlaying()
        {
        }

        protected override void OnPlayUpdate(float progress)
        {
            transform.Rotate((Vector3) rotationSpeed * Time.deltaTime);
        }
    }
}