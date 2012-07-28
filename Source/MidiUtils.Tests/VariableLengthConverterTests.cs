using System;
using NUnit.Framework;

namespace MidiUtils.Tests
{
    public class VariableLengthConverterTests
    {
        [Test]
        public void single_byte_value_should_be_correctly_converted()
        {
            var singleByteValue = new byte[] {0x5};

            const int expected = 5;
            int actual = VariableLengthConverter.ToInt32(singleByteValue);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void multiple_bytes_value_should_be_correctly_converted()
        {
            CheckIfMultipleBytesValueIsCorrectlyConverted(new byte[] {0x80, 0x00}, 0);
            CheckIfMultipleBytesValueIsCorrectlyConverted(new byte[] {0xff, 0x7f}, 16383);
            CheckIfMultipleBytesValueIsCorrectlyConverted(new byte[] {0xf3, 0x3f}, 14783);
            CheckIfMultipleBytesValueIsCorrectlyConverted(new byte[] {0xfc, 0x4d}, 15949);
            CheckIfMultipleBytesValueIsCorrectlyConverted(new byte[] {0xFF, 0xFF, 0xFF, 0x7F}, 0x0FFFFFFF);
        }

        private static void CheckIfMultipleBytesValueIsCorrectlyConverted(byte[] value, int expected)
        {
            int actual = VariableLengthConverter.ToInt32(value);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void converting_invalid_value_should_throw_exception()
        {
            var invalidFirstByteValue = new byte[] {0x7f, 0x7f};
            Assert.Throws<ArgumentException>(() => VariableLengthConverter.ToInt32(invalidFirstByteValue));

            var invalidSecondByteValue = new byte[] { 0xff, 0xff };
            Assert.Throws<ArgumentException>(() => VariableLengthConverter.ToInt32(invalidSecondByteValue));            
        }

        [Test]
        public void converting_empty_bytes_table_should_throw_exception()
        {
            var emptyValue = new byte[] {};
            Assert.Throws<ArgumentException>(() => VariableLengthConverter.ToInt32(emptyValue));
        }

        [Test]
        public void value_cannot_be_longer_than_four_bytes()
        {
            var tooLongValue = new byte[] {0xff, 0xff, 0xff, 0xff, 0x7f};
            Assert.Throws<ArgumentException>(() => VariableLengthConverter.ToInt32(tooLongValue));
        }
    }
}