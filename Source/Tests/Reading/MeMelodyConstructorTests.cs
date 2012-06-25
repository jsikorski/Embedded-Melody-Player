using System;
using System.IO;
using EmbeddedMelodyPlayer.Playing;
using EmbeddedMelodyPlayer.Reading;
using NUnit.Framework;

namespace Tests.Reading
{
    public class MeMelodyConstructorTests
    {
        private const string CorrectFilePath = "resources/sample_correct_melody.me";
        private const string IncorrectFilePath = "resources/sample_incorrect_melody.me";

        [Test]
        public void correct_melody_is_constructed_from_correct_data()
        {
            byte[] melodyData = File.ReadAllBytes(CorrectFilePath);

            IMelodyConstructor melodyConstructor = new MeMelodyConstructor();
            Melody melody = melodyConstructor.CreateMelodyFromBytes(melodyData);

            string expected = File.ReadAllText(CorrectFilePath);
            string actual = melody.ToString();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void exception_is_thrown_for_incorrect_data()
        {
            byte[] melodyData = File.ReadAllBytes(IncorrectFilePath);

            IMelodyConstructor melodyConstructor = new MeMelodyConstructor();
            Assert.Throws<ArgumentException>(() => melodyConstructor.CreateMelodyFromBytes(melodyData));
        }
    }
}