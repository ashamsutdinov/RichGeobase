using System;
using JetBrains.Annotations;

namespace GeoLib.Specification
{
    public interface IEntityContext : 
        IUnitOfWork,
        IDisposable
    {
        /// <summary>
        /// Marks an entity to be saved during Commit phase in the current unit of work.
        /// </summary>
        /// <typeparam name="TEntity">An entity type.</typeparam>
        /// <param name="entity">An entity instance to be saved.</param>
        void Add<TEntity>([NotNull] TEntity entity) where TEntity : Entity;

        /// <summary>
        /// Marks an entity to be deleted during Commit phase in the current unit of work.
        /// </summary>
        /// <typeparam name="TEntity">An entity type.</typeparam>
        /// <param name="entity">An entity instance to be deleted.</param>
        void Delete<TEntity>([NotNull] TEntity entity) where TEntity : Entity;
    }
}