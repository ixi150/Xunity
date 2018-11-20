namespace IXI
{
    public class DisableAfter : Playable
    {
        protected override void OnFinishPlaying()
        {
            Deactivate();
        }
    }
}