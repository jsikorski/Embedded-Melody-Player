using System;

namespace EmbeddedMelodyPlayer.Playing
{
    public class MidiMelodyConstructor : IMelodyConstructor
    {
        #region IMelodyConstructor Members

        public MelodyFrament CreateMelodyFragmentFromBytes(byte[] melodyData, bool isItLastFragment)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}