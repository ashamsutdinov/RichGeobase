using Microsoft.Practices.Unity;

namespace GeoLib
{
    public static class UnityResolver
    {
        public static TResult Resolve<TResult>()
        {
            UnityContainer uc = new UnityContainer();
            return uc.Resolve<TResult>();
        }
    }
}
