using System;
using System.Text;
using EmbeddedMelodyPlayer.Playing;

namespace EmbeddedMelodyPlayer.Reading
{
    public class MeMelodyConstructor : IMelodyConstructor
    {
        public Melody CreateMelodyFromBytes(byte[] melodyData)
        {
            MelodyElement[] melodyElements;
            try
            {
                var melodyString = GetMelodyString(melodyData);
                melodyElements = GetMelodyElementsFromString(melodyString);
            }
            catch
            {
                throw new ArgumentException("Melody data are invalid.");
            }
            
            return new Melody(melodyElements);
        }

        private string GetMelodyString(byte[] melodyData)
        {
            var melodyString = new string(Encoding.UTF8.GetChars(melodyData));
            return melodyString;
        }

        private MelodyElement[] GetMelodyElementsFromString(string melodyString)
        {
            string[] melodyElementsStrings = melodyString.Split(' ');
            
            var melodyElements = new MelodyElement[melodyElementsStrings.Length];
            for (int i = 0; i < melodyElementsStrings.Length; i++)
            {
                melodyElements[i] = MelodyElement.Parse(melodyElementsStrings[i].Trim());
            }

            return melodyElements;
        }
    }
}