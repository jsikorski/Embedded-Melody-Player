using System;
using System.IO;
using GHIElectronics.NETMF.IO;
using Microsoft.SPOT.IO;

namespace EmbeddedMelodyPlayer.Reading
{
    public class SdFileReader
    {
        public byte[] ReadFile(string fileName)
        {
            var sdStorage = new PersistentStorage("SD");
            sdStorage.MountFileSystem();

            var fullFilePath = GetFullFilePath(fileName);
            byte[] fileData = File.ReadAllBytes(fullFilePath);

            sdStorage.UnmountFileSystem();
            return fileData;
        }

        private static string GetFullFilePath(string fileName)
        {
            string rootDirectoryPath = VolumeInfo.GetVolumes()[0].RootDirectory;
            string fullFilePath = rootDirectoryPath + fileName;
            return fullFilePath;
        }
    }
}