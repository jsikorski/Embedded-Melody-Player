namespace EmbeddedMelodyPlayer.Playing
{
    public class MelodyCostructorProvider
    {
        public IMelodyConstructor GetMelodyConstructor()
        {
            return new MeMelodyConstructor();
        }
    }
}