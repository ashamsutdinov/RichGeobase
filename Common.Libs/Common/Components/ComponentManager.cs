using System;
using System.Linq;
using System.Reflection;
using Common.Contracts;
using Common.Utils;

namespace Common.Components
{
    public abstract class ComponentManager :
        IComponentManager
    {
        #region Private

        private static readonly Type ComponentManagerType;

        #endregion

        #region Implementation if IComponentManager

        public abstract TInterface GetComponent<TInterface>()
            where TInterface : IComponent;

        #endregion

        #region ctor

        static ComponentManager()
        {
            var allTypes = Assembly.GetExecutingAssembly().GetExportedTypes();
            var cMgrType = typeof (ComponentManager);
            var componentManagers = allTypes.Where(t => cMgrType.IsAssignableFrom(t) && t != cMgrType).ToArray();
            if (componentManagers.Length > 1)
                throw new Exception("There are too many component managers");
            ComponentManagerType = componentManagers.FirstOrDefault();
        }

        #endregion

        #region Singletone

        public static IComponentManager Instance
        {
            get { return SingletoneFactory.GetInstance<ComponentManager>(ComponentManagerType); }
        }

        #endregion
    }
}