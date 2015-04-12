using System;
using Common.Contracts;

namespace Common.Cache.Contracts
{
    public interface ICache : 
        IComponent,
        IEventDispatcher<ICacheItemEvent>,
        IEventDispatcher<ICacheTagEvent>
    {
        TObject Get<TObject>(string key);

        void Add<TObject>(string key, TObject value, params string[] tags);

        void Add<TObject>(string key, TObject value, TimeSpan lifetime, params string[] tags);

        void Delete(params string[] key);

        void DeleteByTags(params string[] tags);

        TObject Get<TObject>(string key, Func<TObject> creator, params string[] tags);

        TObject Get<TObject>(string key, Func<TObject> creator, TimeSpan lifetime, params string[] tags);
    }
}
