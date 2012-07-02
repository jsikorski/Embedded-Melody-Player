using System;
using EmbeddedMelodyPlayer.Utils;

namespace EmbeddedMelodyPlayer.Infrastructure
{
    public class CommandsPipe : ICommand
    {
        protected ICommand[] _commands;

        public CommandsPipe(ICommand[] commands)
        {
            _commands = commands;
        }

        protected CommandsPipe()
        {
        }

        #region ICommand Members

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

        #endregion
    }
}