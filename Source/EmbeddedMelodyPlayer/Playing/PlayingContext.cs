using System.IO;
using System.Threading;
using EmbeddedMelodyPlayer.Infrastructure;
using Microsoft.SPOT.IO;

namespace EmbeddedMelodyPlayer.Playing
{
    public class PlayingContext : IFailureDetector
    {
        public PlayingContext(VolumeInfo sdCardVolume)
        {
            SdCardVolume = sdCardVolume;

            PreviousMelodyFragmentRememberedEvent = new AutoResetEvent(false);
            PreviousMelodyFragmentPlayedEvent = new AutoResetEvent(false);
            LastMelodyFragmentPlayedEvent = new AutoResetEvent(false);
        }

        public VolumeInfo SdCardVolume { get; private set; }

        public FileStream MelodyFileStream { get; set; }
        public byte[] MelodyFileChunkData { get; set; }
        public bool WasEntireMelodyFileRead { get; set; }
        public MelodyFrament MelodyFragment { get; set; }

        public AutoResetEvent PreviousMelodyFragmentRememberedEvent { get; private set; }
        public AutoResetEvent PreviousMelodyFragmentPlayedEvent { get; private set; }
        public AutoResetEvent LastMelodyFragmentPlayedEvent { get; private set; }

        #region IFailureDetector Members

        public bool FailureDetected { get; set; }

        #endregion
    }
}