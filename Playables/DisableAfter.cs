namespace Xunity.Playables
{
    public class DisableAfter : Playable
    {
        protected override void OnFinishPlaying()
        {
            Deactivate();
        }
    }
}