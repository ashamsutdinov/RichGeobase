namespace Common.Contracts
{
    public interface ICommand<out TResultData>
    {
        ICommandResult<TResultData> Execute();
    }
}