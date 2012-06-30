using System.Threading;
using EmbeddedMelodyPlayer.Infrastructure;
using GHIElectronics.NETMF.FEZ;
using GHIElectronics.NETMF.Hardware;
using Microsoft.SPOT;

namespace EmbeddedMelodyPlayer.Commands
{
    public class PlayMelody : ICommand
    {
        private static readonly PWM Pwm = new PWM((PWM.Pin) FEZ_Pin.PWM.Di5);
        private readonly CurrentContext _currentContext;

        public PlayMelody(CurrentContext currentContext)
        {
            _currentContext = currentContext;
        }

        public void Execute()
        {
            Debug.Print("Playing melody...");
            
            _currentContext.IsPlaying = true;
            _currentContext.Melody.Play(Pwm);
            _currentContext.IsPlaying = false;
        }
    }
}