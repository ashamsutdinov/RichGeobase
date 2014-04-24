//using System.Linq;
//using JetBrains.Annotations;
//using LittleSister.WriteWright.Domain.Core;
//using LittleSister.WriteWright.Domain.Criterions;
//using LittleSister.WriteWright.Domain.Entities;

//namespace RichGeobase.Query.Queries
//{
//    [UsedImplicitly]
//    internal class GetEssayByIdQuery : BaseQuery, ISingleSelectionQuery<GetByIdCriterion, Essay>
//    {
//        public Essay Execute(GetByIdCriterion context)
//        {
//            return QueryProvider.Query<Essay>().Single(x => x.Id == context.Id);
//        }
//    }
//}
