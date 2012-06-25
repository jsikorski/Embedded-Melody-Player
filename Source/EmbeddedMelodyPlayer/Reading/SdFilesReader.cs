using System;
using System.IO;
using System.Threading;
using GHIElectronics.NETMF.IO;
using Microsoft.SPOT.IO;

namespace EmbeddedMelodyPlayer.Reading
{
    public class SdFilesReader
    {
        public byte[] ReadFile(string fileName)
        {
            byte[] fileData;

            using (var sdStorage = new PersistentStorage("SD"))
            {
                sdStorage.MountFileSystem();

                var fullFilePath = GetFullFilePath(fileName);
                fileData = File.ReadAllBytes(fullFilePath);

                Thread.Sleep(500);
                sdStorage.UnmountFileSystem();
            }

            return fileData;
        }

        private static string GetFullFilePath(string fileName)
        {
            string rootDirectoryPath = VolumeInfo.GetVolumes()[0].RootDirectory;
            string fullFilePath = rootDirectoryPath + @"\" + fileName;
            return fullFilePath;
        }
    }
}