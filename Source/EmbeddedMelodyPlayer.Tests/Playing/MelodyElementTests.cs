using System;
using EmbeddedMelodyPlayer.Playing;
using NUnit.Framework;

namespace EmbeddedMelodyPlayer.Tests.Playing
{
    public class MelodyElementTests
    {
        [Test]
        public void parse_throws_exception_for_too_long_melody_element_string()
        {
            Assert.Throws<ArgumentException>(() => MelodyElement.Parse(" C[4] "));
        }

        [Test]
        public void parse_throws_exception_for_invalid_melody_element_symbol()
        {
            Assert.Throws<ArgumentException>(() => MelodyElement.Parse("T[2]"));         
        }

        [Test]
        public void parse_throws_exception_for_invalid_melody_string_format()
        {
            Assert.Throws<ArgumentException>(() => MelodyElement.Parse("C(1]"));
            Assert.Throws<ArgumentException>(() => MelodyElement.Parse("T[4)"));
        }

        [Test]
        public void parse_throws_exception_for_invalid_melody_element_duration()
        {
            Assert.Throws<ArgumentException>(() => MelodyElement.Parse("C[0]"));
            Assert.Throws<ArgumentException>(() => MelodyElement.Parse("C[5]"));
            Assert.Throws<ArgumentException>(() => MelodyElement.Parse("C[9]"));
        }

        [Test]
        public void parse_returns_correct_note_for_note_string()
        {
            MelodyElement melodyElement = MelodyElement.Parse("C[4]");
            Assert.IsInstanceOf<Note>(melodyElement);
            Assert.AreEqual(melodyElement.Symbol, 'C');
            Assert.AreEqual(melodyElement.Duration, 4);
        }

        [Test]
        public void parse_returns_correct_pause_for_pause_string()
        {
            MelodyElement melodyElement = MelodyElement.Parse("P[2]");
            Assert.IsInstanceOf<Pause>(melodyElement);
            Assert.AreEqual(melodyElement.Symbol, 'P');
            Assert.AreEqual(melodyElement.Duration, 2);
        }

        [Test]
        public void to_string_returns_correct_melody_element_string()
        {
            var melodyElement1 = new Note('C', 4);
            string expected = "C[4]";
            string actual = melodyElement1.ToString();
            Assert.AreEqual(expected, actual);

            var melodyElement2 = new Pause(4);
            expected = "P[4]";
            actual = melodyElement2.ToString();
            Assert.AreEqual(expected, actual);
        }
    }
}
