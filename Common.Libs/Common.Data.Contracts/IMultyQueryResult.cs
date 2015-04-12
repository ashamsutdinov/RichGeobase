using System.Collections.Generic;
using Common.Contracts;

namespace Common.Data.Contracts
{
    public interface IMultyQueryResult<TObject> :
        ICommandResult<IEnumerable<TObject>>
        where TObject : ITransientObject
    {
        int Count { get; }

        int TotalCount { get; }
    }
}