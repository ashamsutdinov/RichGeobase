using System;
using System.Collections;
using System.Reflection.Emit;

namespace Common.Utils
{
    public class ObjectFactory
    {
        #region Protected

        protected static readonly IDictionary Cache = Hashtable.Synchronized(new Hashtable());

        #endregion
    }

    public class ObjectFactory<T> :
        ObjectFactory
        where T : new()
    {
        #region Private

        private static Func<T> _createObject;

        #endregion

        #region Public members

        public static Func<T> CreateObject
        {
            get
            {
                if (_createObject != null)
                    return _createObject;
                _createObject = CreateDelegateAndCache(Cache);
                return _createObject;
            }
        }

        #endregion

        #region Utils

        private static Func<T> CreateDelegate()
        {
            return () => new T();
        }

        private static Func<T> CreateDelegateAndCache(IDictionary cache)
        {
            var t = typeof(T);

            var cacheKey = t;

            var c = cache[cacheKey] as Func<T>;
            if (c != null)
            {
                return c;
            }

            lock (cache.SyncRoot)
            {
                c = cache[cacheKey] as Func<T>;
                if (c != null)
                {
                    return c;
                }
                c = CreateDelegate();
                cache.Add(cacheKey, c);
            }
            return c;
        }

        #endregion
    }

    public class ObjectFactory<T, TP1> :
        ObjectFactory
    {
        #region Private

        private static Func<TP1, T> _createObject;

        #endregion

        #region Public members

        public static Func<TP1, T> CreateObject
        {
            get
            {
                if (_createObject != null)
                    return _createObject;
                _createObject = CreateDelegateAndCache(Cache);
                return _createObject;
            }
        }

        #endregion

        #region Utils

        private static Func<TP1, T> CreateDelegate()
        {
            var objType = typeof(T);
            var ctorParamType = typeof(TP1);
            Type[] types = { ctorParamType };
            var objTypeCtor = objType.GetConstructor(types);
            if (objTypeCtor == null)
                throw new Exception("Object do not have ctor with 1 parameter");

            var dynMethod = new DynamicMethod(string.Format("DM$OBJ_FACTORY_{0}_{1}", objType.Name, ctorParamType.Name), objType, types, objType);
            var ilGen = dynMethod.GetILGenerator();
            ilGen.Emit(OpCodes.Ldarg_0);
            ilGen.Emit(OpCodes.Newobj, objTypeCtor);
            ilGen.Emit(OpCodes.Ret);
            return (Func<TP1, T>)dynMethod.CreateDelegate(typeof(Func<TP1, T>));
        }

        private static Func<TP1, T> CreateDelegateAndCache(IDictionary cache)
        {
            var tp1 = typeof(TP1);
            var t = typeof(T);

            var cacheKey = new Tuple<Type, Type>(tp1, t);

            var c = cache[cacheKey] as Func<TP1, T>;
            if (c != null)
            {
                return c;
            }

            lock (cache.SyncRoot)
            {
                c = cache[cacheKey] as Func<TP1, T>;
                if (c != null)
                {
                    return c;
                }
                c = CreateDelegate();
                cache.Add(cacheKey, c);
            }
            return c;
        }

        #endregion
    }
}
