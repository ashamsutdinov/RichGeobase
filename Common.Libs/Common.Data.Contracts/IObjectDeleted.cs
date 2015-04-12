using Common.Contracts;

namespace Common.Data.Contracts
{
    public interface IObjectDeleted<out TObject> : 
        IEvent
        where TObject : ITransientObject
    {
        IDeleteResult<TObject> Result { get; } 
    }
}