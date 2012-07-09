using System;
using EmbeddedMelodyPlayer.Playing;
using NUnit.Framework;

namespace EmbeddedMelodyPlayer.Tests.Playing
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
    }
}