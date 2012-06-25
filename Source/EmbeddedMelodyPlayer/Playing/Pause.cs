using System.Threading;
using GHIElectronics.NETMF.Hardware;

namespace EmbeddedMelodyPlayer.Playing
{
    public class Pause : MelodyElement
    {
        public Pause(int duration)
        {
           CheckElementParameters('P', duration);

            Symbol = 'P';
           Duration = duration;
        }

        public override void Play(PWM output)
        {
            Thread.Sleep(Duration * 250);
        }
    }
}