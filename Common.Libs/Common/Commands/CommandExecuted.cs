using Common.Contracts;
using Common.Events;

namespace Common.Commands
{
    public class CommandExecuted :
        Event,
        ICommandExecuted
    {
        public CommandExecuted(object sender, ICommand command, ICommandResult result) : 
            base(sender)
        {
            ExecutedCommand = command;
            Result = result;
        }

        public ICommand ExecutedCommand { get; private set; }

        public ICommandResult Result { get; private set; }

    }
}