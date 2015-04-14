using Common.Contracts;

namespace Common.Commands
{
    public abstract class CommandResult :
        ICommandResult
    {
        public ITransactionResult TransactionResult { get; internal set; }
    }
}