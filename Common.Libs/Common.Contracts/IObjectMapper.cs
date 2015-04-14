namespace Common.Contracts
{
    public interface IObjectMapper : 
        IComponent
    {
        TObject2 Map<TObject1, TObject2>(TObject1 source);
    }
}