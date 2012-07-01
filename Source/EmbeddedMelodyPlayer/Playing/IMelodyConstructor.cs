using EmbeddedMelodyPlayer.Playing;

namespace EmbeddedMelodyPlayer.Reading
{
    public interface IMelodyConstructor
    {
        MelodyFrament CreateMelodyFromBytes(byte[] melodyData);
    }
}