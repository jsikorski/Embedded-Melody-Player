using EmbeddedMelodyPlayer.Playing;
using Microsoft.SPOT.IO;

namespace EmbeddedMelodyPlayer
{
    public class CurrentContext
    {
        public VolumeInfo SdCardVolume { get; set; }
        public byte[] MelodyData { get; set; }
        public Melody Melody { get; set; }
        public bool IsPlaying { get; set; }
    }
}