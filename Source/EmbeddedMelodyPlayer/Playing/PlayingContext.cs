using System.IO;
using System.Threading;

namespace EmbeddedMelodyPlayer.Playing
{
    public class PlayingContext
    {
        public PlayingContext()
        {
            CanPlay = new AutoResetEvent(true);
            WasLastMelodyFragmentPlayed = new AutoResetEvent(false);
        }

        public byte[] MelodyFileChunkData { get; set; }
        public MelodyFrament MelodyFrament { get; set; }
        public FileStream MelodyFileStream { get; set; }
        public AutoResetEvent CanPlay { get; private set; }
        public bool WasEntireMelodyFileRead { get; set; }
        public AutoResetEvent WasLastMelodyFragmentPlayed { get; set; }
    }
}