using EmbeddedMelodyPlayer.Playing;

namespace EmbeddedMelodyPlayer.Reading
{
    public interface IMelodyConstructor
    {
        Melody CreateMelodyFromBytes(byte[] melodyData);
    }
}