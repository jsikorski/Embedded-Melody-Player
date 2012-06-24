using System;
using EmbeddedMelodyPlayer.Commands;

namespace EmbeddedMelodyPlayer.Infrastructure
{
    public class CommandInvoker
    {
        public void InvokeCommand(ICommand command)
        {
            try
            {
                command.Execute();
            }
            catch (Exception)
            {
                
            }
        }
    }
}