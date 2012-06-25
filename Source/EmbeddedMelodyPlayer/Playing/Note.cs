namespace EmbeddedMelodyPlayer.Playing
{
    public class Note : MelodyElement
    {
        public Note(char symbol, int duration)
        {
            CheckInputData(symbol, duration);

            Symbol = symbol;
            Duration = duration;
        }
    }
}