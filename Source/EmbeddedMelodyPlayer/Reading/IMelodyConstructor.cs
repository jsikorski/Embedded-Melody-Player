using EmbeddedMelodyPlayer.Playing;

namespace EmbeddedMelodyPlayer.Reading
{
    public interface IMelodyConstructor
    {
        Melody CreateMelodyFromFile();
    }
}