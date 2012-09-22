using System.Threading;
using EmbeddedMelodyPlayer.Core;
using EmbeddedMelodyPlayer.Infrastructure;
using GHIElectronics.NETMF.FEZ;
using GHIElectronics.NETMF.Hardware;
using I2C.Expander;
using Microsoft.SPOT;

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