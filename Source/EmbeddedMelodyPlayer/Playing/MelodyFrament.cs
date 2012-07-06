using VikingErik.NetMF.MicroLinq;

namespace EmbeddedMelodyPlayer.Playing
{
    public class MelodyFrament
    {
        private readonly MelodyElement[] _melodyElements;

        public bool IsFirst { get; private set; }
        public bool IsLast { get; private set; }

        public MelodyFrament(MelodyElement[] melodyElements, bool isFirst, bool isLast)
        {
            _melodyElements = melodyElements;
            IsFirst = isFirst;
            IsLast = isLast;
        }

        public override string ToString()
        {
            string result = string.Empty;
            for (int i = 0; i < _melodyElements.Count(); i++)
            {
                if (i == 0)
                    result += _melodyElements[i];
                else
                    result += " " + _melodyElements[i];
            }

            return result;
        }

        public void Play()
        {
            for (int i = 0; i < _melodyElements.Count(); i++)
            {
                _melodyElements[i].Play();
            }
        }
    }
}