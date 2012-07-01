using EmbeddedMelodyPlayer.Playing;
using Microsoft.SPOT.IO;

namespace EmbeddedMelodyPlayer.Core
{
    public class ProgramState
    {
        public bool IsPlaying { get; set; }
        public VolumeInfo SdCardVolume { get; set; }
    }
}