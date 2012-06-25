namespace EmbeddedMelodyPlayer.Playing
{
    public class Pause : MelodyElement
    {
        public Pause(int duration)
        {
           CheckInputData('P', duration);

            Symbol = 'P';
           Duration = duration;
        }
    }
}