using Common.Components;
using Common.Contracts;

namespace Common.Utils
{
    public class DuplexObjectConverter<TInterface1, TInterface2> :
        IDuplexObjectConverter<TInterface1, TInterface2>
    {
        #region Private

        private readonly IObjectMapper _objectMapper = ComponentManager.Instance.GetComponent<IObjectMapper>();

        #endregion

        public TInterface1 Convert(TInterface2 interface2)
        {
            return _objectMapper.Map<TInterface2, TInterface1>(interface2);
        }

        public TInterface2 Convert(TInterface1 interface1)
        {
            return _objectMapper.Map<TInterface1, TInterface2>(interface1);
        }
    }

    public class DuplexObjectConverter<TObject1, TInterface1, TInterface2> :
        IDuplexObjectConverter<TInterface1,TInterface2>
        where TObject1 : class, TInterface1
    {
        #region Private

        private readonly IObjectMapper _objectMapper = ComponentManager.Instance.GetComponent<IObjectMapper>();  

        #endregion

        #region Implementation of IDuplexObjectConverter

        public TInterface1 Convert(TInterface2 interface2)
        {
            return _objectMapper.Map<TInterface2, TObject1>(interface2);
        }

        public TInterface2 Convert(TInterface1 interface1)
        {
            var obj1 = interface1 as TObject1;
            return _objectMapper.Map<TObject1, TInterface2>(obj1);
        }

        #endregion
    }

    public class DuplexObjectConverter<TInterface1, TObject1, TInterface2, TObject2> :
        IDuplexObjectConverter<TInterface1, TInterface2>
        where TObject1 : class, TInterface1
        where TObject2 : class, TInterface2
    {
        #region Private

        private readonly IObjectMapper _objectMapper = ComponentManager.Instance.GetComponent<IObjectMapper>();

        #endregion

        #region Implementation of IDuplexObjectConverter

        public TInterface1 Convert(TInterface2 interface2)
        {
            var obj2 = interface2 as TObject2;
            return _objectMapper.Map<TObject2, TObject1>(obj2);
        }

        public TInterface2 Convert(TInterface1 interface1)
        {
            var obj1 = interface1 as TObject1;
            return _objectMapper.Map<TObject1, TObject2>(obj1);
        }

        #endregion
    }
}