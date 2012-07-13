using System;
using GHIElectronics.NETMF.FEZ;
using GHIElectronics.NETMF.Hardware;

namespace EmbeddedMelodyPlayer.Infrastructure
{
    public class BusyScope : IDisposable
    {
        private static readonly PWM Blue = new PWM((PWM.Pin)FEZ_Pin.PWM.Di8);
        private static readonly PWM Green = new PWM((PWM.Pin)FEZ_Pin.PWM.Di9);
        private static readonly PWM Red = new PWM((PWM.Pin)FEZ_Pin.PWM.Di10);

        private readonly IFailureDetector _failureDetector;

        static BusyScope()
        {
            TurnGreenLedOn();
        }

        public BusyScope(IFailureDetector failureDetector)
        {
            _failureDetector = failureDetector;
            IsBusy = true;

            TurnBlueLedOn();
        }

        public bool IsBusy { get; private set; }

        public void Dispose()
        {
            IsBusy = false;

            if (_failureDetector.FailureDetected)
                TurnRedLedOn();
            else
                TurnGreenLedOn();
        }

        private static void TurnRedLedOn()
        {
            Red.Set(20000, 100);
            Green.Set(false);
            Blue.Set(false);
        }

        private static void TurnGreenLedOn()
        {
            Red.Set(false);
            Green.Set(20000, 100);
            Blue.Set(false);
        }

        private static void TurnBlueLedOn()
        {
            Red.Set(false);
            Green.Set(false);
            Blue.Set(20000, 100);
        }
    }
}