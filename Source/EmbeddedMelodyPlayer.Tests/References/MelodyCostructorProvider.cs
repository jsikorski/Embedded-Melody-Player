namespace EmbeddedMelodyPlayer.Reading
{
    public class MelodyCostructorProvider
    {
        public IMelodyConstructor GetMelodyConstructor()
        {
            return new MeMelodyConstructor();
        }
    }
}