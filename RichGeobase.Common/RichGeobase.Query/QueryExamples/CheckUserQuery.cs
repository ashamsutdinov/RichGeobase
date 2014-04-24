//using System.Linq;
//using LittleSister.WriteWright.Domain.Core;
//using LittleSister.WriteWright.Domain.Criterions.Account;
//using LittleSister.WriteWright.Domain.Entities;

//namespace RichGeobase.Query.Queries
//{
//    class CheckUserQuery : BaseQuery, ISingleSelectionQuery<CheckUserCriterion, User>
//    {
//        public User Execute(CheckUserCriterion criterion)
//        {
//            return QueryProvider.Query<User>().SingleOrDefault(x => x.Email == criterion.NameUser);
//        }
//    }
//}
