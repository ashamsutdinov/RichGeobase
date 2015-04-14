using System;
using Common.Contracts;

namespace Common.Commands
{
    public class TransactionResult : 
        ITransactionResult
    {
        public bool IsCommitedSuccessfully { get; private set; }

        public Exception Error { get; private set; }

        public TransactionResult(bool isCommitedSucessfully, Exception error)
        {
            IsCommitedSuccessfully = isCommitedSucessfully;
            Error = error;
        }
    }
}
