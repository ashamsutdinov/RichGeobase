using System;
using System.Linq;
using JetBrains.Annotations;

namespace GeoLib.Specification
{
    public interface IDbContext : 
        IDisposable
    {
        [NotNull]
        IQueryable<TResult> Query<TResult>() where TResult : class;
    }
}
