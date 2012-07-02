using VikingErik.NetMF.MicroLinq;

namespace EmbeddedMelodyPlayer.Playing
{
    public class MelodyFrament
    {
        private readonly MelodyElement[] _melodyElements;

        public MelodyFrament(MelodyElement[] melodyElements, bool isItLastFragment)
        {
            _melodyElements = melodyElements;
            IsItLastFragment = isItLastFragment;
        }

        public bool IsItLastFragment { get; private set; }

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