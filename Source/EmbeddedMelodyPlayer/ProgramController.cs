using EmbeddedMelodyPlayer.Commands;
using EmbeddedMelodyPlayer.Infrastructure;
using EmbeddedMelodyPlayer.Playing;
using EmbeddedMelodyPlayer.Reading;
using GHIElectronics.NETMF.FEZ;
using GHIElectronics.NETMF.Hardware;
using Microsoft.SPOT.Hardware;

namespace EmbeddedMelodyPlayer
{
    public class ProgramController
    {
        public void Start()
        {
            var currentContext = new CurrentContext();
            ReadMelodyData(currentContext);
            ConstructMelody(currentContext);
            PlayMelody(currentContext);
        }

        private static void ReadMelodyData(CurrentContext currentContext)
        {
            ICommand readMelodyData = new ReadMelodyData(currentContext);
            CommandInvoker.InvokeCommand(readMelodyData);
        }

        private static void ConstructMelody(CurrentContext currentContext)
        {
            ICommand constructMelody = new ConstructMelody(currentContext);
            CommandInvoker.InvokeCommand(constructMelody);
        }

        private static void PlayMelody(CurrentContext currentContext)
        {
            ICommand playMelody = new PlayMelody(currentContext);
            CommandInvoker.InvokeCommand(playMelody);
        }
    }
}