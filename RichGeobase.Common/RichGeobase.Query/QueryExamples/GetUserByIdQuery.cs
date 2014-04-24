//using System.Linq;
//using JetBrains.Annotations;
//using LittleSister.WriteWright.Domain.Core;
//using LittleSister.WriteWright.Domain.Criterions;
//using LittleSister.WriteWright.Domain.Entities;
//using RichGeobase.Query.Interface;

//namespace RichGeobase.Query.Queries
//{
//    [UsedImplicitly]
//    internal class GetUserByIdQuery : BaseQuery, ISingleSelectionQuery<GetByIdCriterion, User>
//    {
//        public User Execute(GetByIdCriterion context)
//        {
//            return QueryProvider.Query<User>().Single(x => x.Id == context.Id);
//        }
//    }
//}
