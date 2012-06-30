using System;
using EmbeddedMelodyPlayer.Infrastructure;
using EmbeddedMelodyPlayer.Reading;
using Microsoft.SPOT;

namespace EmbeddedMelodyPlayer.Commands
{
    public class ReadMelodyData : ICommand
    {
        private const string MelodyFileName = "melody.me";

        private readonly CurrentContext _currentContext;
        private readonly SdFilesReader _sdFilesReader;

        public ReadMelodyData(CurrentContext currentContext)
        {
            _currentContext = currentContext;
            _sdFilesReader = new SdFilesReader(currentContext.SdCardVolume);
        }

        public void Execute()
        {
            Debug.Print("Reading melody data from SD card...");

            _currentContext.MelodyData = _sdFilesReader.ReadFile(MelodyFileName);
        }
    }
}