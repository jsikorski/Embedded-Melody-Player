using System.IO;
using EmbeddedMelodyPlayer.Infrastructure;
using EmbeddedMelodyPlayer.Playing;
using Microsoft.SPOT;
using Microsoft.SPOT.IO;

namespace EmbeddedMelodyPlayer.Commands
{
    public class ReadMelodyFileChunk : ICommand
    {
        private const string MelodyFileName = "melody.me";
        private const int FileChunkLength = 20;

        private readonly PlayingContext _playingContext;
        private readonly VolumeInfo _sdCardVolume;

        public ReadMelodyFileChunk(VolumeInfo sdCardVolume, PlayingContext playingContext)
        {
            _sdCardVolume = sdCardVolume;
            _playingContext = playingContext;
        }

        #region ICommand Members

        public void Execute()
        {
            Debug.Print("Reading melody file chunk from SD card...");
            _playingContext.MelodyFileChunkData = ReadFileChunk(MelodyFileName);
        }

        #endregion

        private byte[] ReadFileChunk(string fileName)
        {
            string fullFilePath = GetFullFilePath(fileName);

            CheckIfItIsFirstChunk(fullFilePath);

            var fileData = new byte[FileChunkLength];
            int readBytesNumber = _playingContext.MelodyFileStream.Read(fileData, 0, FileChunkLength);

            CheckIfItWasLastChunk(readBytesNumber);

            return fileData;
        }

        private void CheckIfItIsFirstChunk(string fullFilePath)
        {
            if (_playingContext.MelodyFileStream == null)
                _playingContext.MelodyFileStream = new FileStream(
                    fullFilePath, FileMode.Open, FileAccess.Read, FileShare.Read, 64);
        }

        private void CheckIfItWasLastChunk(int readBytesNumber)
        {
            if (readBytesNumber < FileChunkLength)
            {
                _playingContext.MelodyFileStream.Dispose();
                _playingContext.MelodyFileStream = null;
                _playingContext.WasEntireMelodyFileRead = true;
            }
        }

        private string GetFullFilePath(string fileName)
        {
            string rootDirectoryPath = _sdCardVolume.RootDirectory;
            string fullFilePath = rootDirectoryPath + @"\" + fileName;
            return fullFilePath;
        }
    }
}