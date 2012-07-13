using EmbeddedMelodyPlayer.Infrastructure;
using NSubstitute;
using NUnit.Framework;

namespace EmbeddedMelodyPlayer.Tests.Infrastructure
{
    public class CommandsInvokerTests
    {
        [Test]
        public void execute_should_execute_command_execute()
        {
            var command = Substitute.For<ICommand>();
            CommandsInvoker.Execute(command);
            command.Received().Execute();
        }
    }
}