namespace Common.Data.Contracts
{
    public interface IObjectFactory
    {
        TObjectInterface Get<TObjectInterface>()
            where TObjectInterface : ITransientObject;
    }
}