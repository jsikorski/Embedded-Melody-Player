using System;

namespace MidiReader
{
    public static class VariableLengthConverter
    {
        private const int MaxVariableLengthLength = 4;

        public static int ToInt32(byte[] bytes)
        {
            CheckInputArgument(bytes);

            int value = 0;
            for (int i = 0; i < bytes.Length; i++)
            {
                byte currentByte = bytes[i];

                bool isItLAstByteInTable = i == bytes.Length - 1;
                CheckIfByteIsCorrect(currentByte, isItLAstByteInTable);
                
                int variableLengthCurrentByteValue = currentByte & 0x7f;
                value <<= 7;
                value |= variableLengthCurrentByteValue;
            }

            return value;
        }

        private static void CheckInputArgument(byte[] bytes)
        {
            CheckIfInputIsNotEmptyArray(bytes);
            CheckInputArgumentLength(bytes);
        }

        private static void CheckIfInputIsNotEmptyArray(byte[] bytes)
        {
            if (bytes.Length < 1)
                throw new ArgumentException("Cannot convert empty bytes table to variable-length value.");
        }

        private static void CheckInputArgumentLength(byte[] bytes)
        {
            if (bytes.Length > MaxVariableLengthLength)
                throw new ArgumentException("Variable-length value cannot be longer than four bytes.");
        }

        private static void CheckIfByteIsCorrect(byte @byte, bool isItLastByteInTable)
        {
            bool shouldItBeLastByteByFlag = (@byte & 0x7f) == @byte;
            if (shouldItBeLastByteByFlag != isItLastByteInTable)
                throw new ArgumentException("Incorrect variable-length notation.");
        }
    }
}