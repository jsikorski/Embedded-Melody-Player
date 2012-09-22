using System.IO;
using EmbeddedMelodyPlayer.Infrastructure;
using EmbeddedMelodyPlayer.Playing;
using EmbeddedMelodyPlayer.Reading;
using Microsoft.SPOT;
using Microsoft.SPOT.IO;

namespace EmbeddedMelodyPlayer.Commands
{
    public class ReadMelodyFileChunk : ICommand
    {
        private readonly PlayingContext _playingContext;
        private readonly MelodyFileReader _melodyFileReader;

        public ReadMelodyFileChunk(VolumeInfo sdCardVolume, PlayingContext playingContext)
        {
            _playingContext = playingContext;
            _melodyFileReader = new MelodyFileReader(sdCardVolume, playingContext);
        }

        public void Execute()
        {
            Debug.Print("Reading melody file chunk from SD card...");

            _playingContext.MelodyFileChunkData = _melodyFileReader.ReadNextFileChunk();
        }
    }
}