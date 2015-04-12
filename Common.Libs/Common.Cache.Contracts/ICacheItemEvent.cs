namespace Common.Cache.Contracts
{
    public interface ICacheItemEvent :
        ICacheEvent
    {
        string Key { get; }

        ICacheItem Item { get; }
    }
}