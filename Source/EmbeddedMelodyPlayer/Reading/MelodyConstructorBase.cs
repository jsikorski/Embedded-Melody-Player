namespace EmbeddedMelodyPlayer.Reading
{
    public abstract class MelodyConstructorBase
    {
        protected readonly FileReader FileReader;

        protected MelodyConstructorBase()
        {
            FileReader = new FileReader();
        }
    }
}