using System;
using System.Threading;
using EmbeddedMelodyPlayer.Core;
using EmbeddedMelodyPlayer.Pins;
using EmbeddedMelodyPlayer.Utils;
using GHIElectronics.NETMF.FEZ;
using GHIElectronics.NETMF.IO;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using Microsoft.SPOT.IO;

namespace EmbeddedMelodyPlayer
{
    public class Program
    {
        public static void Main()
        {
            var programController = new ProgramController();
            programController.Start();

            Debug.Print("Program started and ready.");
            Thread.Sleep(Timeout.Infinite);
        }
    }
}
