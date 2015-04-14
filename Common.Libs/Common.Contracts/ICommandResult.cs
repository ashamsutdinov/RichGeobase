namespace Common.Contracts
{
    public interface ICommandResult
    {
        ITransactionResult TransactionResult { get; }
    }

    public interface ICommandResult<out TResultData> : 
        ICommandResult
    {
        TResultData Data { get; }
    }
}