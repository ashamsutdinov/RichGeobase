using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;

namespace RichGeobase.Common
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
