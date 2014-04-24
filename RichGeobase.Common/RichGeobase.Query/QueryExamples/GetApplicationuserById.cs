//using System.Linq;
//using JetBrains.Annotations;
//using LittleSister.WriteWright.Domain.Core;
//using LittleSister.WriteWright.Domain.Criterions;
//using LittleSister.WriteWright.Domain.Entities;

//namespace RichGeobase.Query.Queries
//{
//    [UsedImplicitly]
//    internal class GetApplicationuserById : BaseQuery, ISingleSelectionQuery<GetByIdCriterion, ApplicationUser>
//    {
//        public ApplicationUser Execute(GetByIdCriterion context)
//        {
//            return QueryProvider.Query<ApplicationUser>().Single(x => x.Id == context.Id);
//        }
//    }
//}
