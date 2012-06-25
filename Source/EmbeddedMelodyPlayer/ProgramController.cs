using EmbeddedMelodyPlayer.Commands;
using EmbeddedMelodyPlayer.Infrastructure;
using EmbeddedMelodyPlayer.Playing;
using EmbeddedMelodyPlayer.Reading;

namespace EmbeddedMelodyPlayer
{
    public class ProgramController
    {
        public void Start()
        {
            var currentContext = new CurrentContext();
            ReadMelodyData(currentContext);
            ConstructMelody(currentContext);

            string melodyString = currentContext.Melody.ToString();
        }

        private static void ReadMelodyData(CurrentContext currentContext)
        {
            ICommand readMelodyData = new ReadMelodyData(currentContext);
            CommandInvoker.InvokeCommand(readMelodyData);
        }

        private static void ConstructMelody(CurrentContext currentContext)
        {
            var constructMelody = new ConstructMelody(currentContext);
            CommandInvoker.InvokeCommand(constructMelody);
        }
    }
}