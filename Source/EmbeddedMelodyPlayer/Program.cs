using System.Threading;
using EmbeddedMelodyPlayer.Core;
using GHIElectronics.NETMF.FEZ;
using GHIElectronics.NETMF.Hardware;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;

namespace EmbeddedMelodyPlayer
{
    public class Program
    {
        private static readonly PWM Red = new PWM((PWM.Pin)FEZ_Pin.PWM.Di10);
        private static readonly PWM Green = new PWM((PWM.Pin)FEZ_Pin.PWM.Di9);
        private static readonly PWM Blue = new PWM((PWM.Pin)FEZ_Pin.PWM.Di8);

        public static void Main()
        {
            Blue.Set(10000, 100);

            //var programController = new ProgramController();
            //programController.Start();


            Debug.Print("Program started and ready.");
            Thread.Sleep(Timeout.Infinite);
        }
    }
}