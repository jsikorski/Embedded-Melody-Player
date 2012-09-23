using System;
using GHIElectronics.NETMF.FEZ;
using GHIElectronics.NETMF.Hardware;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;

namespace EmbeddedMelodyPlayer.Playing
{
    public static class MelodyElementDurationResolver
    {
        private static int _multiplierBase = 250;
        private const int PhotoresistorFactor = 2;

        private static readonly AnalogIn AnalogIn = new AnalogIn(AnalogIn.Pin.Ain0);
        
        private static readonly InterruptPort TempoUpButton = 
            new InterruptPort((Cpu.Pin)FEZ_Pin.Digital.Di11, false, Port.ResistorMode.PullUp, Port.InterruptMode.InterruptEdgeLow);
        private static readonly InterruptPort TempoDownButton = 
            new InterruptPort((Cpu.Pin)FEZ_Pin.Digital.Di12, false, Port.ResistorMode.PullUp, Port.InterruptMode.InterruptEdgeLow);

        private static DateTime _lastTempoUpEventTime;
        private static DateTime _lastTempoDownEventTime;

        static MelodyElementDurationResolver()
        {
            AnalogIn.SetLinearScale(0, 100);
            _lastTempoUpEventTime = DateTime.Now;
            _lastTempoDownEventTime = DateTime.Now;
            TempoUpButton.OnInterrupt += OnTempoUpButtonPress;
            TempoDownButton.OnInterrupt += OnTempoDownButtonPress;
        }

        private static void OnTempoUpButtonPress(uint data1, uint data2, DateTime time)
        {
            if (time - _lastTempoUpEventTime < new TimeSpan(0, 0, 0, 0, 500))
                return;

            _lastTempoUpEventTime = time;

            if (_multiplierBase <= 200)
                return;

            _multiplierBase -= 25;
            Debug.Print("Melody tempo increased. Actual multiplier base value is " + _multiplierBase + ".");
        }

        private static void OnTempoDownButtonPress(uint data1, uint data2, DateTime time)
        {
            if (time - _lastTempoDownEventTime < new TimeSpan(0, 0, 0, 0, 500))
                return;

            _lastTempoDownEventTime = time;

            _multiplierBase += 25;
            Debug.Print("Melody tempo decreased. Actual multiplier base value is " + _multiplierBase + ".");
        }

        public static int GetElementDuration(MelodyElement melodyElement)
        {
            return melodyElement.Duration * (_multiplierBase - PhotoresistorFactor * AnalogIn.Read());
        }
    }
}