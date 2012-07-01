using System;
using EmbeddedMelodyPlayer.Utils;
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
                Debug.GC(true);
            }
            catch (Exception exception)
            {
                DebugHelper.PrintCommandExceptionMessage(exception, command);
            }
        }
    }
}