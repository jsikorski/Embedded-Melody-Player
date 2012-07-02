using EmbeddedMelodyPlayer.Infrastructure;
using Microsoft.SPOT.IO;

namespace EmbeddedMelodyPlayer.Core
{
    public class ProgramState
    {
        public BusyScope BusyScope { get; set; }
        public VolumeInfo SdCardVolume { get; set; }
    }
}