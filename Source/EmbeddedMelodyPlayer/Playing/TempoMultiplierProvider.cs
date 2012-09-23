using GHIElectronics.NETMF.Hardware;

namespace EmbeddedMelodyPlayer.Playing
{
    public static class TempoMultiplierProvider
    {
        private const int MultiplierBase = 250;
        private const int MultiplierFactor = 2;

        private static readonly AnalogIn AnalogIn = new AnalogIn(AnalogIn.Pin.Ain0);

        static TempoMultiplierProvider()
        {
            AnalogIn.SetLinearScale(0, 100);
        }

        public static int GetMultiplier()
        {
            return MultiplierBase - MultiplierFactor * AnalogIn.Read();
        }
    }
}