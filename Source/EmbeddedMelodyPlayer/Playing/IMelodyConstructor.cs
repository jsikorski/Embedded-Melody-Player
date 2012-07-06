namespace EmbeddedMelodyPlayer.Playing
{
    public interface IMelodyConstructor
    {
        MelodyFrament CreateMelodyFragmentFromBytes(byte[] melodyData, bool isFirst, bool isLast);
    }
}