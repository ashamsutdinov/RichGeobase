//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Linq.Expressions;
//using JetBrains.Annotations;
//using LittleSister.WriteWright.Domain.Core;
//using LittleSister.WriteWright.Domain.Criterions.EssayCriterrions;
//using LittleSister.WriteWright.Domain.Entities;

//namespace RichGeobase.Query.Queries
//{
//    [UsedImplicitly]
//    internal class GetEssaysPagedAndSortedQuery : BaseGridQuery<GetEssayPagedAndSortCriterion, Essay>, IMultipleSelectionQuery<GetEssayPagedAndSortCriterion, Essay>
//    {
//        #region Implementation of IQuery<in GetEssayPagedAndSortCriterion,out IEnumerable<Essay>>

//        public IEnumerable<Essay> Executes(GetEssayPagedAndSortCriterion criterion)
//        {
//            Expression<Func<Essay, bool>> expressionForWhere = t => t.UserId == criterion.UserId; 

//            criterion.CountRows = GetCountRows(expressionForWhere);

//            return PrepareQueryableForGrid(criterion, expressionForWhere)
//                .ToArray();
//        }

//        #endregion
//    }
//}
