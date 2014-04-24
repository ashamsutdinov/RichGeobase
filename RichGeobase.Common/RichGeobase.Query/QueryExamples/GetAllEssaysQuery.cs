//using System.Collections.Generic;
//using System.Linq;
//using JetBrains.Annotations;
//using LittleSister.WriteWright.Domain.Core;
//using LittleSister.WriteWright.Domain.Criterions;
//using LittleSister.WriteWright.Domain.Entities;

//namespace RichGeobase.Query.Queries
//{
//    [UsedImplicitly]
//    internal class GetAllEssaysQuery : BaseQuery, IMultipleSelectionQuery<GetAllCriterion, Essay>
//    {
//        public IEnumerable<Essay> Execute(GetAllCriterion context)
//        {
//            return QueryProvider.Query<Essay>().ToArray();
//        }
//    }
//}