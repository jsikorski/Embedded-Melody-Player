namespace EmbeddedMelodyPlayer.Playing
{
    public class Pause : IMelodyElement
    {
        public char Symbol
        {
            get { return 'P'; }
        }

        public int Duration { get; private set; }

        public Pause(int duration)
        {
            Duration = duration;
        }
    }
}