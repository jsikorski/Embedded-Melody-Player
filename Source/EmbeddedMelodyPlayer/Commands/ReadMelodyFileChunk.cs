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
        private const int FileStreamBufferSize = FileChunkLength;

        private readonly PlayingContext _playingContext;
        private readonly VolumeInfo _sdCardVolume;

        public ReadMelodyFileChunk(VolumeInfo sdCardVolume, PlayingContext playingContext)
        {
            _sdCardVolume = sdCardVolume;
            _playingContext = playingContext;
        }

        public void Execute()
        {
            Debug.Print("Reading melody file chunk from SD card...");
            _playingContext.MelodyFileChunkData = ReadFileChunk(MelodyFileName);
        }

        private byte[] ReadFileChunk(string fileName)
        {
            string fullFilePath = GetFullFilePath(fileName);

            OpenFileStreamForFirstChunk(fullFilePath);

            var fileData = new byte[FileChunkLength];
            int readBytesNumber = _playingContext.MelodyFileStream.Read(fileData, 0, FileChunkLength);

            CheckStreamForLastChunk(readBytesNumber);

            return fileData;
        }

        private string GetFullFilePath(string fileName)
        {
            string rootDirectoryPath = _sdCardVolume.RootDirectory;
            string fullFilePath = rootDirectoryPath + @"\" + fileName;
            return fullFilePath;
        }

        private void OpenFileStreamForFirstChunk(string fullFilePath)
        {
            if (IsItFirstFileChunk())
                _playingContext.MelodyFileStream = new FileStream(
                    fullFilePath, FileMode.Open, FileAccess.Read, FileShare.Read, FileStreamBufferSize);
        }

        private bool IsItFirstFileChunk()
        {
            return _playingContext.MelodyFileStream == null;
        }

        private void CheckStreamForLastChunk(int readBytesNumber)
        {
            if (readBytesNumber < FileChunkLength)
            {
                _playingContext.MelodyFileStream.Dispose();
                _playingContext.MelodyFileStream = null;
                _playingContext.WasEntireMelodyFileRead = true;
            }
        }
    }
}