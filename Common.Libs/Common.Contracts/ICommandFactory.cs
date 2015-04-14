namespace Common.Contracts
{
    public interface ICommandFactory : 
        IEventDispatcher<ICommandExecuted>,
        IComponent
    {
        TCommand Create<TCommand, TInputData, TResult>(TInputData input)
            where TCommand : ICommand<TInputData, TResult>
            where TInputData : ICommandInput
            where TResult : ICommandResult;
    }
}