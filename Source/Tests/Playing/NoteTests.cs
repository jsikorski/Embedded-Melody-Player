using System;
using EmbeddedMelodyPlayer.Playing;
using GHIElectronics.NETMF.Hardware;
using NSubstitute;
using NUnit.Framework;

namespace Tests.Playing
{
    public class NoteTests
    {
        [Test]
        public void cannot_create_note_with_invalid_symbol()
        {
            Assert.Throws<ArgumentException>(() => new Note('T', 4));
        }

        [Test]
        public void cannot_create_note_with_invalid_duration()
        {
            Assert.Throws<ArgumentException>(() => new Note('C', 5));
        }

        [Test]
        public void it_is_possible_to_create_correct_note()
        {
            Assert.DoesNotThrow(() => new Note('C', 4));
        }

        [Test]
        public void play_call_pwm_set()
        {
            var pwm = Substitute.For<PWM>();

            var note = new Note('C', 4);
            note.Play(pwm);

            pwm.Received().Set(262, 50);
        }

        [Test]
        public void play_left_pwm_set_in_false_state()
        {
            var pwm = Substitute.For<PWM>();

            var note = new Note('C', 4);
            note.Play(pwm);

            pwm.Received().Set(false);
        }
    }
}