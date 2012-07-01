using System;
using System.Threading;
using EmbeddedMelodyPlayer.Utils;
using Microsoft.SPOT;

namespace EmbeddedMelodyPlayer.Infrastructure
{
    public class CommandsPipe : ICommand
    {
        private readonly ICommand[] _commands;

        public CommandsPipe(ICommand[] commands)
        {
            _commands = commands;
        }

        public void Execute()
        {
            for (int i = 0; i < _commands.Length; i++)
            {
                ICommand currentCommand = _commands[i];

                try
                {
                    currentCommand.Execute();
                }
                catch (Exception exception)
                {
                    DebugHelper.PrintCommandExceptionMessage(exception, currentCommand);
                    throw new Exception("Cannot invoke commands pipe.");
                }
            }
        }
    }
}