using System.IO;
using EmbeddedMelodyPlayer.Core;
using EmbeddedMelodyPlayer.Infrastructure;
using EmbeddedMelodyPlayer.Playing;
using Microsoft.SPOT;
using Microsoft.SPOT.IO;

namespace EmbeddedMelodyPlayer.Commands
{
    public class ReadMelodyData : ICommand
    {
        private const string MelodyFileName = "melody.me";
        private const int FileChunkLength = 20;

        private readonly VolumeInfo _sdCardVolume;
        private readonly PlayingContext _playingContext;

        public ReadMelodyData(VolumeInfo sdCardVolume, PlayingContext playingContext)
        {
            _sdCardVolume = sdCardVolume;
            _playingContext = playingContext;
        }

        public void Execute()
        {
            Debug.Print("Reading melody data from SD card...");
            _playingContext.MelodyFileChunkData = ReadFile(MelodyFileName);
        }

        private byte[] ReadFile(string fileName)
        {
            var fullFilePath = GetFullFilePath(fileName);

            if (_playingContext.MelodyFileStream == null)
                _playingContext.MelodyFileStream = new FileStream(fullFilePath, FileMode.Open, FileAccess.Read, FileShare.Read, 64);

            var fileData = new byte[FileChunkLength];
            int readBytesNumber = _playingContext.MelodyFileStream.Read(fileData, 0, FileChunkLength);

            if (readBytesNumber < FileChunkLength)
            {
                _playingContext.MelodyFileStream.Dispose();
                _playingContext.MelodyFileStream = null;
                _playingContext.WasEntireMelodyFileRead = true;
            }

            return fileData;
        }

        private string GetFullFilePath(string fileName)
        {
            string rootDirectoryPath = _sdCardVolume.RootDirectory;
            string fullFilePath = rootDirectoryPath + @"\" + fileName;
            return fullFilePath;
        }
    }
}