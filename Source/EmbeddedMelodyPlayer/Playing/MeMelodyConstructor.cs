using System;
using System.Text;

namespace EmbeddedMelodyPlayer.Playing
{
    public class MeMelodyConstructor : IMelodyConstructor
    {
        #region IMelodyConstructor Members

        public MelodyFrament CreateMelodyFragmentFromBytes(byte[] melodyData, bool isItLastFragment)
        {
            MelodyElement[] melodyElements;
            try
            {
                string melodyString = GetMelodyString(melodyData);
                melodyElements = GetMelodyElementsFromString(melodyString);
            }
            catch
            {
                throw new ArgumentException("MelodyFrament data are invalid.");
            }

            return new MelodyFrament(melodyElements, isItLastFragment);
        }

        #endregion

        private string GetMelodyString(byte[] melodyData)
        {
            var melodyString = new string(Encoding.UTF8.GetChars(melodyData));
            return melodyString.Trim();
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