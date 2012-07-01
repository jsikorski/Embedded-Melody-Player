using EmbeddedMelodyPlayer.Playing;
using GHIElectronics.NETMF.FEZ;
using GHIElectronics.NETMF.Hardware;
using Microsoft.SPOT.Hardware;

namespace EmbeddedMelodyPlayer.Pins
{
    public static class Outputs
    {
        public static readonly PWM BuzzerPwm = new PWM((PWM.Pin)FEZ_Pin.PWM.Di10);

        private static readonly OutputPort[] NotesLeds = new[]
                                                             {
                                                                 new OutputPort((Cpu.Pin) FEZ_Pin.Digital.Di0, false),
                                                                 new OutputPort((Cpu.Pin) FEZ_Pin.Digital.Di1, false),
                                                                 new OutputPort((Cpu.Pin) FEZ_Pin.Digital.Di11, false),
                                                                 new OutputPort((Cpu.Pin) FEZ_Pin.Digital.Di12, false),
                                                                 new OutputPort((Cpu.Pin) FEZ_Pin.Digital.Di4, false),
                                                                 new OutputPort((Cpu.Pin) FEZ_Pin.Digital.Di5, false),
                                                                 new OutputPort((Cpu.Pin) FEZ_Pin.Digital.Di6, false),
                                                                 new OutputPort((Cpu.Pin) FEZ_Pin.Digital.Di7, false),
                                                             };

        public static void TurnNotesLedsByNote(Note note)
        {
            for (int i = 0; i <= note.ToNumber(); i++)
            {
                NotesLeds[i].Write(true);
            }
        }

        public static void TurnOffAllNotesLeds()
        {
            for (int i = 0; i < NotesLeds.Length; i++)
            {
                NotesLeds[i].Write(false);
            }
        }
    }
}