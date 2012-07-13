using System.Collections;
using System.Threading;
using GHIElectronics.NETMF.FEZ;
using GHIElectronics.NETMF.Hardware;
using I2C.Expander;

namespace EmbeddedMelodyPlayer.Playing
{
    public class Note : MelodyElement
    {
        private const int BuzzerPwmDutyCycle = 50;
        private const int TempoMultiplier = 100;

        private static readonly PWM BuzzerPwm = new PWM((PWM.Pin)FEZ_Pin.PWM.Di5);

        private static readonly I2CExpander I2CExpander = new I2CExpander(pinsMode: 0x20);

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

        private static readonly IDictionary NotesBytes = new Hashtable
                                                             {
                                                                 {'C', (byte) 0x01},
                                                                 {'D', (byte) 0x03},
                                                                 {'E', (byte) 0x07},
                                                                 {'F', (byte) 0x0f},
                                                                 {'G', (byte) 0x1f},
                                                                 {'A', (byte) 0x3f},
                                                                 {'H', (byte) 0x7f},
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

            BuzzerPwm.Set(frequency, BuzzerPwmDutyCycle);
            TurnNoteLedsOn();

            Thread.Sleep(Duration * TempoMultiplier);

            TurnAllNoteLedsOff();
            BuzzerPwm.Set(false);
        }

        private void TurnNoteLedsOn()
        {
            I2CExpander.Write((byte) NotesBytes[Symbol]);
        }

        private void TurnAllNoteLedsOff()
        {
            I2CExpander.Write(0x00);
        }
    }
}