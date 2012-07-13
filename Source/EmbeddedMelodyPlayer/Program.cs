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
        //PRIVATE STATIC READONLY PWM BLUE = NEW PWM((PWM.PIN)FEZ_PIN.PWM.DI8);
        //PRIVATE STATIC READONLY PWM GREEN = NEW PWM((PWM.PIN)FEZ_PIN.PWM.DI9);
        //PRIVATE STATIC READONLY PWM RED = NEW PWM((PWM.PIN)FEZ_PIN.PWM.DI10);

        public static void Main()
        {
            var programController = new ProgramController();
            programController.Start();

            Debug.Print("Program started and ready.");
            Thread.Sleep(Timeout.Infinite);
        }
    }
}