using Common.Contracts;

namespace Common.Cache.Contracts
{
    public interface ICacheDataProvider :
        IEventHandler<ICacheItemEvent>,
        IEventHandler<ICacheTagEvent>,
        IEventDispatcher<ICacheItemEvent>,
        IEventDispatcher<ICacheTagEvent>
    {
        
    }
}