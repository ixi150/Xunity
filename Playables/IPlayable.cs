namespace Xunity.Playables
{
    public interface IPlayable
    {
        bool IsPlaying { get; }
        void Play();
        void Play(out bool success);
        void Stop();
        void Stop(out bool success);
    }
}