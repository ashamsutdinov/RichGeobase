using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace RichGeobase.Query
{
    public interface IDbContext
    {
        [NotNull]
        IQueryable<TResult> Query<TResult>() where TResult : class;
    }
}
