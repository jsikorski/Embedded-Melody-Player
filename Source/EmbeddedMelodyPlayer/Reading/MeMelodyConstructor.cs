using System;
using System.Collections;
using System.Linq;
using System.Text;
using EmbeddedMelodyPlayer.Playing;

namespace EmbeddedMelodyPlayer.Reading
{
    public class MeMelodyConstructor : MelodyConstructorBase, IMelodyConstructor
    {
        private const string FileName = "melody.me";

        public Melody CreateMelodyFromFile()
        {
            var melodyString = GetMelodyString();
            IEnumerable melodyElements = GetMelodyElementsFromString(melodyString);
            return new Melody(melodyElements);
        }

        private string GetMelodyString()
        {
            byte[] fileData = FileReader.ReadFileFromSD(FileName);
            var melodyString = new string(Encoding.UTF8.GetChars(fileData));
            return melodyString;
        }

        private IEnumerable GetMelodyElementsFromString(string melodyString)
        {
            string[] melodyElementsStrings = melodyString.Split(' ');
            return melodyElementsStrings.Select(GetMelodyElementFromString);
        }

        private IMelodyElement GetMelodyElementFromString(string melodyElementString)
        {
            char melodyElementSymbol = melodyElementString[0];
            string melodyElementDurationString = melodyElementString.Substring(1);
            int melodyElementDuration = Convert.ToInt32(melodyElementDurationString.Trim('[', ']'));

            if (melodyElementSymbol == 'P')
                return new Pause(melodyElementDuration);

            return new Note(melodyElementSymbol, melodyElementDuration);
        }

    }
}