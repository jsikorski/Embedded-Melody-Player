using System.Collections;
using System.Threading;
using GHIElectronics.NETMF.FEZ;
using GHIElectronics.NETMF.Hardware;
using Microsoft.SPOT.Hardware;

namespace EmbeddedMelodyPlayer.Playing
{
    public class Note : MelodyElement
    {
        private const int BuzzerPwmDutyCycle = 50;
        private const int TempoMultiplier = 30;

        //private static readonly PWM BuzzerPwm = new PWM((PWM.Pin)FEZ_Pin.PWM.Di10);

        private static readonly OutputPort[] NotesLeds = new[]
                                                             {
                                                                 new OutputPort((Cpu.Pin) FEZ_Pin.Digital.Di0, false),
                                                                 new OutputPort((Cpu.Pin) FEZ_Pin.Digital.Di1, false),
                                                                 new OutputPort((Cpu.Pin) FEZ_Pin.Digital.Di2, false),
                                                                 new OutputPort((Cpu.Pin) FEZ_Pin.Digital.Di3, false),
                                                                 new OutputPort((Cpu.Pin) FEZ_Pin.Digital.Di4, false),
                                                                 new OutputPort((Cpu.Pin) FEZ_Pin.Digital.Di5, false),
                                                                 new OutputPort((Cpu.Pin) FEZ_Pin.Digital.Di6, false),
                                                                 new OutputPort((Cpu.Pin) FEZ_Pin.Digital.Di7, false),
                                                             };

        private static readonly IDictionary NotesFrequencies = new Hashtable
                                                                   {
                                                                       {'C', 262},
                                                                       {'D', 294},
                                                                       {'E', 330},
                                                                       {'F', 350},
                                                                       {'G', 392},
                                                                       {'A', 440},
                                                                       {'H', 494}
                                                                   };

        private static readonly IDictionary NotesNumbers = new Hashtable
                                                               {
                                                                   {'C', 0},
                                                                   {'D', 1},
                                                                   {'E', 2},
                                                                   {'F', 3},
                                                                   {'G', 4},
                                                                   {'A', 5},
                                                                   {'H', 6},
                                                               };


        public Note(char symbol, int duration)
        {
            CheckElementParameters(symbol, duration);

            Symbol = symbol;
            Duration = duration;
        }

        public override void Play()
        {
            var frequency = (int)NotesFrequencies[Symbol];

            //BuzzerPwm.Set(frequency, BuzzerPwmDutyCycle);
            TurnNoteLedsOn();

            Thread.Sleep(Duration * TempoMultiplier);

            TurnAllNoteLedsOff();
            //BuzzerPwm.Set(false);
        }

        private void TurnNoteLedsOn()
        {
            for (int i = 0; i <= ToNumber(); i++)
            {
                NotesLeds[i].Write(true);
            }
        }

        private int ToNumber()
        {
            return (int)NotesNumbers[Symbol];
        }

        private void TurnAllNoteLedsOff()
        {
            for (int i = 0; i < NotesLeds.Length; i++)
            {
                NotesLeds[i].Write(false);
            }
        }
    }
}