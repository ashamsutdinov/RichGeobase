namespace Common.Data.Contracts
{
    public interface IDeleteResult<out TObject> :
        ISingleQueryResult<TObject>
        where TObject : ITransientObject
    {
        
    }
}