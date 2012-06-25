﻿
using System.Collections;
using GHIElectronics.NETMF.Hardware;
using VikingErik.NetMF.MicroLinq;

namespace EmbeddedMelodyPlayer.Playing
{
    public class Melody
    {
        private readonly MelodyElement[] _melodyElements;

        public Melody(MelodyElement[] melodyElements)
        {
            _melodyElements = melodyElements;
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

        public void Play(PWM output)
        {
            for (int i = 0; i < _melodyElements.Count(); i++)
            {
                _melodyElements[i].Play(output);
            }
        }
    }
}