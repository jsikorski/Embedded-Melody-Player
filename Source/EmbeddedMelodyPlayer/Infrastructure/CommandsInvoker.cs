using System;
using Microsoft.SPOT;

namespace EmbeddedMelodyPlayer.Infrastructure
{
    public static class CommandsInvoker
    {
        public static void ExecuteCommand(ICommand command)
        {
            try
            {
                command.Execute();
            }
            catch (Exception exception)
            {
                Debug.Print("Exception: " + exception.Message);
            }
        }
    }
}