namespace Xunity.Playables
{
    public class DisableAfter : Playable
    {
        protected override void OnStartPlaying()
        {
        }
        
        protected override void OnPlayUpdate(float progress)
        {
        }

        protected override void OnStoppedPlaying()
        {
        }

        protected override void OnFinishPlaying()
        {
            Deactivate();
        }

    }
}