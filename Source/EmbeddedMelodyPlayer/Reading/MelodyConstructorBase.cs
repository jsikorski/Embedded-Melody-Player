namespace EmbeddedMelodyPlayer.Reading
{
    public abstract class MelodyConstructorBase
    {
        protected readonly SdFileReader SdFileReader;

        protected MelodyConstructorBase()
        {
            SdFileReader = new SdFileReader();
        }
    }
}