namespace Common.Data.Contracts
{
    public interface ISaveResult<out TObject> :
        ISingleQueryResult<TObject>
        where TObject : ITransientObject
    {
        TObject ObjectBeforeSave { get; }

        bool IsNew { get; }
    }
}