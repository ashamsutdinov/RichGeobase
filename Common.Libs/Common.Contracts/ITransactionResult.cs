using System;

namespace Common.Contracts
{
    public interface ITransactionResult
    {
        bool IsCommitedSuccessfully { get; }

        Exception Error { get; }
    }
}