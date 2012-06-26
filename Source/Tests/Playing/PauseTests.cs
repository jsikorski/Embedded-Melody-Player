using System;
using EmbeddedMelodyPlayer.Playing;
using GHIElectronics.NETMF.Hardware;
using NSubstitute;
using NUnit.Framework;

namespace Tests.Playing
{
    public class PauseTests
    {
        [Test]
        public void cannot_create_pause_with_invalid_duration()
        {
            Assert.Throws<ArgumentException>(() => new Pause(5));
        }

        [Test]
        public void it_is_possible_to_create_correct_pause()
        {
            Assert.DoesNotThrow(() => new Pause(4));
        }
 
        [Test]
        public void play_does_not_call_pwm_set()
        {
            var pwm = Substitute.For<PWM>();

            var pause = new Pause(4);
            pause.Play(pwm);

            pwm.DidNotReceive().Set(Arg.Any<bool>());
            pwm.DidNotReceive().Set(Arg.Any<int>(), Arg.Any<int>());
        }
    }
}