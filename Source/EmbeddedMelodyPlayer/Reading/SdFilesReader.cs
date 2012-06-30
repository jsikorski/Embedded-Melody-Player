using System.IO;
using Microsoft.SPOT.IO;

namespace EmbeddedMelodyPlayer.Reading
{
    public class SdFilesReader
    {
        private readonly VolumeInfo _sdCardVolume;

        public SdFilesReader(VolumeInfo sdCardVolume)
        {
            _sdCardVolume = sdCardVolume;
        }

        public byte[] ReadFile(string fileName)
        {
            var fullFilePath = GetFullFilePath(fileName);
            byte[] fileData = File.ReadAllBytes(fullFilePath);

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