using System;
using EmbeddedMelodyPlayer.Utils;
using Microsoft.SPOT;

namespace EmbeddedMelodyPlayer.Infrastructure
{
    public static class CommandsInvoker
    {
        public static void ExecuteCommand(ICommand command, Action onFailure = null)
        {
            try
            {
                command.Execute();
            }
            catch (Exception exception)
            {
                DebugHelper.PrintCommandExceptionMessage(exception, command);

                if (onFailure != null)
                    onFailure();
            }
        }
    }
}