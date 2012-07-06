namespace EmbeddedMelodyPlayer.Infrastructure
{
    public interface IFailureDetector
    {
        bool FailureDetected { get; } 
    }
}