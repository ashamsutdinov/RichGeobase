using System.Collections.Generic;
using System.Data.Entity;
using GeoLib;
using GeoLib.Specification;
using RichGeobase.Query.Interface;

namespace RichGeobase.Account.Dal.UnitsOfWork
{
    internal class BaseUnitOfWork : IUnitOfWork
    {
        private readonly EfDbContext _context;

        public BaseUnitOfWork()
        {
            _context = new EfDbContext();
        }

        public void Add<TEntity>(TEntity entity) where TEntity : Entity
        {
            DbSet<TEntity> dbSet = _context.Set<TEntity>();
            dbSet.Add(entity);
        }

        public void Delete<TEntity>(TEntity entity) where TEntity : Entity
        {
            DbSet<TEntity> dbSet = _context.Set<TEntity>();
            dbSet.Remove(entity);
        }

        public int QueryCount<TCriterion>(TCriterion criterion) where TCriterion : ICriterion
        {
            ISingleSelectionQuery<TCriterion, int> selectionQuery = UnityResolver.Resolve<ISingleSelectionQuery<TCriterion, int>>();
            return selectionQuery.Execute(criterion);
        }

        public TResult QuerySingle<TCriterion, TResult>(TCriterion criterion) where TCriterion : ICriterion
        {
            ISingleSelectionQuery<TCriterion, TResult> selectionQuery = UnityResolver.Resolve<ISingleSelectionQuery<TCriterion, TResult>>();
            return selectionQuery.Execute(criterion);
        }

        public IEnumerable<TResult> QueryMultiple<TCriterion, TResult>(TCriterion criterion) where TCriterion : ICriterion
        {
            IMultipleSelectionQuery<TCriterion, TResult> selectionQuery = UnityResolver.Resolve<IMultipleSelectionQuery<TCriterion, TResult>>();
            return selectionQuery.Execute(criterion);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public void Commit()
        {
            _context.SaveChanges();
        }
    }
}