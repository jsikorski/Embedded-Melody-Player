using System.Collections;
using System.Threading;
using GHIElectronics.NETMF.Hardware;

namespace EmbeddedMelodyPlayer.Playing
{
    public class Note : MelodyElement
    {
        private readonly IDictionary _notesFrequencies = new Hashtable
                                                             {
                                                                 {'C', 262},
                                                                 {'D', 294},
                                                                 {'E', 330},
                                                                 {'F', 350},
                                                                 {'G', 392},
                                                                 {'A', 440},
                                                                 {'H', 494}
                                                             };

        public Note(char symbol, int duration)
        {
            CheckElementParameters(symbol, duration);

            Symbol = symbol;
            Duration = duration;
        }

        public override void Play(PWM output)
        {
            var frequency = (int) _notesFrequencies[Symbol];
            output.Set(frequency, 50);
            Thread.Sleep(Duration * 200);
            output.Set(false);
        }
    }
}