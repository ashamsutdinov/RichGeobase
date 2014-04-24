using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using JetBrains.Annotations;
using RichGeobase.Query.Criterions;
using RichGeobase.Query.Helpers;
using RichGeobase.Query.Interface;

namespace RichGeobase.Query
{
    internal interface IBaseGridQuery<TBaseGridCriterion, TBaseEntity>
        where TBaseGridCriterion : BaseGridCriterion<TBaseEntity>
        where TBaseEntity : BusinessEntity
    {
    }

    [UsedImplicitly]
    internal class BaseGridQuery<TBaseGridCriterion, TBaseEntity> : BaseQuery, IBaseGridQuery<TBaseGridCriterion, TBaseEntity>, IMultipleSelectionQuery<TBaseGridCriterion, TBaseEntity>
        where TBaseGridCriterion : BaseGridCriterion<TBaseEntity>
        where TBaseEntity : BusinessEntity
    {

        public IEnumerable<TBaseEntity> Execute(TBaseGridCriterion criterion)
        {
            return PrepareQueryableForGrid(criterion, criterion.ExpressionForWhere)
                .ToArray();
        }

        protected IQueryable<TBaseEntity> PrepareQueryableForGrid(TBaseGridCriterion criterion)
        {
            return PrepareQueryableForGrid(criterion, null);
        }

        protected IQueryable<TBaseEntity> PrepareQueryableForGrid(TBaseGridCriterion criterion, Expression<Func<TBaseEntity, bool>> expressionForInitialWhere)
        {
            criterion.CountRows = GetCountRows(expressionForInitialWhere);

            var query = QueryProvider
                .Query<TBaseEntity>()
                .Where(GetExpression(expressionForInitialWhere))
                .Sort(GetSortColumnSafe(criterion.SortColumn), criterion.IsAscending)
                .Page(criterion.PageNumber, criterion.RowsPerPage);
            return query;
        }

        private Expression<Func<TBaseEntity, bool>> GetExpression(Expression<Func<TBaseEntity, bool>> expression)
        {
            return expression ?? (t => true);
        }

        protected int GetCountRows(Expression<Func<TBaseEntity, bool>> expression)
        {
            var countRows = QueryProvider
                .Query<TBaseEntity>()
                .Where(GetExpression(expression)).Count();
            return countRows;
        }

        protected int GetCountRows()
        {
            return GetCountRows(null);
        }

        private static string GetSortColumnSafe(string sortColumn)
        {
            return string.IsNullOrWhiteSpace(sortColumn) ? PropertyHelper.GetMemberName((TBaseEntity entity) => entity.Id) : sortColumn;
        }
    }
}
