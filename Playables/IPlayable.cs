namespace Xunity.Playables
{
    public interface IPlayable
    {
        bool IsPlaying { get; }
        bool Play();
        bool Stop();
    }
}