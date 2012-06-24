
namespace EmbeddedMelodyPlayer.Playing
{
    public interface IMelodyElement
    {
        char Symbol { get; }
        int Duration { get; }
    }
}