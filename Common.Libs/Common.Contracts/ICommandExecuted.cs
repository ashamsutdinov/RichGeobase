namespace Common.Contracts
{
    public interface ICommandExecuted : 
        IEvent
    {
        ICommand ExecutedCommand { get; }

        ICommandResult Result { get; }
    }
}