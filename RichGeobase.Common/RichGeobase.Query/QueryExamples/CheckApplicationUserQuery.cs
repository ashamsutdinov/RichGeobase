//using System.Linq;
//using LittleSister.WriteWright.Domain.Core;
//using LittleSister.WriteWright.Domain.Criterions.Account;
//using LittleSister.WriteWright.Domain.Entities;

//namespace RichGeobase.Query.Queries
//{
//    internal class CheckApplicationUserQuery : BaseQuery, ISingleSelectionQuery<CheckUserCriterion, ApplicationUser>
//    {
//        public ApplicationUser Execute(CheckUserCriterion criterion)
//        {
//            return QueryProvider.Query<ApplicationUser>().SingleOrDefault(x => x.Email == criterion.NameUser);
//        }
//    }
//}
