
using System.Collections;

namespace EmbeddedMelodyPlayer.Playing
{
    public class Melody
    {
        private IEnumerable _melodyElements;

        public Melody(IEnumerable melodyElements)
        {
            _melodyElements = melodyElements;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}