namespace Common.Contracts
{
    public interface IComponentManager
    {
        TInterface GetComponent<TInterface>()
            where TInterface : IComponent;
    }
}