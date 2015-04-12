namespace Common.Contracts
{
    public interface ICommandResult<out TResultData>
    {
        TResultData Data { get; }

        ITransactionResult Result { get; }
    }
}