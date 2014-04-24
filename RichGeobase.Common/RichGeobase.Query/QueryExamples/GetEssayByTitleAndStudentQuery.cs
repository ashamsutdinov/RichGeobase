//using System.Linq;
//using LittleSister.WriteWright.Domain.Core;
//using LittleSister.WriteWright.Domain.Criterions.EssayCriterrions;
//using LittleSister.WriteWright.Domain.Entities;

//namespace RichGeobase.Query.Queries
//{
//    class GetEssayByTitleAndStudentQuery : BaseQuery, ISingleSelectionQuery<GetEssayByTitleAndStudentCriterion, Essay>
//    {
//        public Essay Execute(GetEssayByTitleAndStudentCriterion criterion)
//        {
//           return  QueryProvider.Query<Essay>()
//               .Single(t => t.Title == criterion.TitleEssay 
//                   && t.Student.Email == criterion.EmailStudent 
//                   && t.UserId == criterion.UserId
//                   && t.Student.Name == criterion.NameStudent);
//        }
//    }
//}
