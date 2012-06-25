using GHIElectronics.NETMF.FEZ;
using GHIElectronics.NETMF.Hardware;

namespace EmbeddedMelodyPlayer.Commands
{
    public class PlayMelody : ICommand
    {
        private readonly CurrentContext _currentContext;

        public PlayMelody(CurrentContext currentContext)
        {
            _currentContext = currentContext;
        }

        public void Execute()
        {
            var pwm = new PWM((PWM.Pin)FEZ_Pin.PWM.Di5);
            _currentContext.Melody.Play(pwm);
        }
    }
}