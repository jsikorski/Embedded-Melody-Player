using EmbeddedMelodyPlayer.Playing;
using Microsoft.SPOT.IO;

namespace EmbeddedMelodyPlayer.Reading
{
    public class MelodyFileReaderProvider
    {
         public IMelodyFileReader GetMelodyFileReader(VolumeInfo sdCardVolume, PlayingContext playingContext)
         {
             return new MeMelodyFileReader(sdCardVolume, playingContext);
         }
    }
}