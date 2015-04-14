using System;
using System.Collections;

namespace Common.Utils
{
    public static class SingletoneFactory
    {
        #region Private

        private static readonly IDictionary Cache = Hashtable.Synchronized(new Hashtable());

        #endregion

        #region Public members

        public static TObject GetInstance<TObject>()
            where TObject : class, new()
        {
            var t = typeof (TObject);
            var cached = GetFromCache(t, Cache, () => new TObject());
            var result = cached as TObject;
            if (result != null)
                return result;
            throw new Exception("Failed to resolve singletone instance");
        }

        public static TObject GetInstance<TObject>(Type instanceType)
            where TObject : class 
        {
            var cached = GetFromCache(instanceType, Cache, () => Activator.CreateInstance(instanceType));
            var result = cached as TObject;
            if (result != null)
                return result;
            throw new Exception("Failed to resolve singletone instance");
        }

        #endregion

        #region Utils

        private static object GetFromCache(Type t, IDictionary cache, Func<object> objCreator)
        {
            if (!cache.Contains(t))
            {
                lock (Cache.SyncRoot)
                {
                    if (!cache.Contains(t))
                    {
                        cache[t] = objCreator();
                    }
                }
            }
            return cache[t];
        }

        #endregion
    }
}