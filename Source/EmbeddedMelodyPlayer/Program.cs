using System.Threading;
using EmbeddedMelodyPlayer.Core;
using Microsoft.SPOT;

namespace EmbeddedMelodyPlayer
{
    public class Program
    {
        public static void Main()
        {
            var programController = new ProgramController();
            programController.Start();

            Debug.Print("Program started and ready.");
            Thread.Sleep(Timeout.Infinite);
        }
    }
}