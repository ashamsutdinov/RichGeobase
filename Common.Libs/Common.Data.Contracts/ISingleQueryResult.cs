using Common.Contracts;

namespace Common.Data.Contracts
{
    public interface ISingleQueryResult<out TObject> :
        ICommandResult<TObject>
        where TObject : ITransientObject
    {

    }
}