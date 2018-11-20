namespace IXI.Interfaces
{
    public interface IPlayable
    {
        bool IsPlaying { get; }
        bool Play();
        bool Stop();
    }
}