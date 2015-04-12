using Common.Contracts;

namespace Common.Data.Contracts
{
    public interface IObjectUpdated<out TObject> :
        IEvent
        where TObject : ITransientObject
    {
        ISaveResult<TObject> Result { get; } 
    }
}