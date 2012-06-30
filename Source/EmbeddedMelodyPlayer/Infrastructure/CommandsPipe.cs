using System;
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
                try
                {
                    ICommand command = _commands[i];
                    command.Execute();
                }
                catch (Exception exception)
                {
                    Debug.Print("Exception: " + exception.Message + " in command " + _commands[i] + ".");
                    throw new Exception("Cannot invoke commands pipe.");
                }
            }
        }
    }
}