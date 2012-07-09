using System;
using EmbeddedMelodyPlayer.Playing;
using NUnit.Framework;

namespace EmbeddedMelodyPlayer.Tests.Playing
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
    }
}