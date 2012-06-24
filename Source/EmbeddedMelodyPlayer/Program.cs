using System;
using Microsoft.SPOT;

namespace EmbeddedMelodyPlayer
{
    public class Program
    {
        public static void Main()
        {
            var programController = new ProgramController();
            programController.Start();
        }

    }
}
