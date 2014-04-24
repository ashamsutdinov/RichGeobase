//using System;
//using System.Linq;
//using System.Linq.Expressions;
//using JetBrains.Annotations;
//using LittleSister.WriteWright.Domain.Core;
//using LittleSister.WriteWright.Domain.Criterions;

//namespace RichGeobase.Query.Queries
//{

//    [UsedImplicitly]
//    internal class GetCountForGridQuery<TBaseGridCriterion, TBaseEntity> : BaseQuery, IQuery<TBaseGridCriterion, int>
//        where TBaseGridCriterion : BaseGridCriterion<TBaseEntity>
//        where TBaseEntity : BusinessEntity
//    {

//        public int Execute(TBaseGridCriterion criterion)
//        {
//            return GetCountRows(criterion.ExpressionForWhere);
//        }

//        private Expression<Func<TBaseEntity, bool>> GetExpression(Expression<Func<TBaseEntity, bool>> expression)
//        {
//             return expression ?? (t => true);
//        }

//        protected int GetCountRows(Expression<Func<TBaseEntity, bool>> expression)
//        {
//            var countRows = QueryProvider
//                .Query<TBaseEntity>()
//                .Where(GetExpression(expression)).Count();
//            return countRows;
//        }
        
//        protected int GetCountRows()
//        {
//            return GetCountRows(null);
//        }
//    }
//}


