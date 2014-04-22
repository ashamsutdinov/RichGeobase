using System.Collections.Generic;
using JetBrains.Annotations;

namespace RichGeobase.Query.Interface
{
    public interface IEntityContext
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

        /// <summary>
        /// Runs a query that produces a count integer result.
        /// </summary>
        /// <typeparam name="TCriterion">A criterion type.</typeparam>
        /// <param name="criterion">A criterion for the query</param>
        /// <returns>A count number.</returns>
        /// <remarks>
        /// This method can be used to calculate a number of entities that satisfy certain <see cref="criterion"/> in the context.
        /// </remarks>
        int QueryCount<TCriterion>([NotNull] TCriterion criterion) where TCriterion : ICriterion;

        /// <summary>
        /// Runs a query for a scalar result.
        /// </summary>
        /// <typeparam name="TCriterion">A criterion type.</typeparam>
        /// <typeparam name="TResult">A result type.</typeparam>
        /// <param name="criterion">A criterion for the query.</param>
        /// <returns>A single value result that is satisfy the given <paramref name="criterion"/>.</returns>
        [NotNull]
        TResult QuerySingle<TCriterion, TResult>([NotNull] TCriterion criterion) where TCriterion : ICriterion;

        /// <summary>
        /// Runs a query for a multiple result set.
        /// </summary>
        /// <typeparam name="TCriterion">A criterion type.</typeparam>
        /// <typeparam name="TResult">A result type.</typeparam>
        /// <param name="criterion">An criterion for the query.</param>
        /// <returns>A multiple result set, or empty set if no result satisfy the given <paramref name="criterion"/>.</returns>
        [NotNull]
        IEnumerable<TResult> QueryMultiple<TCriterion, TResult>([NotNull] TCriterion criterion) where TCriterion : ICriterion;
    }
}