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
        private readonly VolumeInfo _sdCardVolume;
        private readonly PlayingContext _playingContext;
        private readonly MelodyFileReaderProvider _melodyFileReaderProvider;

        public ReadMelodyFileChunk(VolumeInfo sdCardVolume, PlayingContext playingContext)
        {
            _sdCardVolume = sdCardVolume;
            _playingContext = playingContext;

            _melodyFileReaderProvider = new MelodyFileReaderProvider();
        }

        public void Execute()
        {
            Debug.Print("Reading melody file chunk from SD card...");

            IMelodyFileReader melodyFileReader = _melodyFileReaderProvider.GetMelodyFileReader(_sdCardVolume, _playingContext);
            _playingContext.MelodyFileChunkData = melodyFileReader.ReadNextFileChunk();
        }
    }
}