namespace Common.Cache.Contracts
{
    public interface ICacheTagEvent :
        ICacheEvent
    {
        string Tag { get; }
    }
}