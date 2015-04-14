namespace Common.Contracts
{
    public interface ICommand
    {
        
    }

    public interface ICommand<in TInputData, out TResult> : 
        ICommand
        where TInputData : ICommandInput
        where TResult : ICommandResult
    {
        TInputData Input { set; }

        TResult Execute();
    }
}