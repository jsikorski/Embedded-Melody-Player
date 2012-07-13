using System;
using EmbeddedMelodyPlayer.Utils;

namespace EmbeddedMelodyPlayer.Infrastructure
{
    public static class CommandsInvoker
    {
        public static void Execute(ICommand command, BasicAction onFailure = null)
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