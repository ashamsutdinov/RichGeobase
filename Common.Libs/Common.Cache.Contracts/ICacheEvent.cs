 using Common.Contracts;

namespace Common.Cache.Contracts
{
    public interface ICacheEvent : 
        IEvent
    {
        CacheEventType Type { get; }
    }
}