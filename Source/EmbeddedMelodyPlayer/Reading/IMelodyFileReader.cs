namespace EmbeddedMelodyPlayer.Reading
{
    public interface IMelodyFileReader
    {
        byte[] ReadNextFileChunk();
    }
}