using System;
using VikingErik.NetMF.MicroLinq;

namespace EmbeddedMelodyPlayer.Playing
{
    public abstract class MelodyElement
    {
        private static readonly char[] AllowedMelodyElementsSymbols = new[] {'C', 'D', 'E', 'F', 'G', 'A', 'H', 'P'};
        private static readonly int[] AllowedDurations = new[] {1, 2, 4, 8};

        // Public for testing purposes
        public char Symbol { get; protected set; }
        public int Duration { get; protected set; }

        public static MelodyElement Parse(string melodyElementString)
        {
            CheckMelodyElementString(melodyElementString);

            char melodyElementSymbol = melodyElementString[0];
            string melodyElementDurationString = melodyElementString.Substring(1);
            int melodyElementDuration = Convert.ToInt32(melodyElementDurationString.Trim('[', ']'));

            if (melodyElementSymbol == 'P')
                return new Pause(melodyElementDuration);

            return new Note(melodyElementSymbol, melodyElementDuration);
        }

        private static void CheckMelodyElementString(string melodyElementString)
        {
            if (melodyElementString.Length != 4 ||
                !AllowedMelodyElementsSymbols.Contains(Convert.ToChar(melodyElementString[0])) ||
                melodyElementString[1] != '[' ||
                !AllowedDurations.Contains(Convert.ToInt32(melodyElementString[2].ToString())) ||
                melodyElementString[3] != ']')
            {
                throw new ArgumentException("MelodyFragment element string is invalid.");
            }
        }

        public override string ToString()
        {
            return Symbol + "[" + Duration + "]";
        }

        protected void CheckElementParameters(char symbol, int duration)
        {
            if (!AllowedMelodyElementsSymbols.Contains(symbol) ||
                !AllowedDurations.Contains(duration))
            {
                throw new ArgumentException("MelodyFragment element parameters are invalid.");
            }
        }

        public abstract void Play();
    }
}