//using System.Linq;
//using LittleSister.WriteWright.Domain.Core;
//using LittleSister.WriteWright.Domain.Criterions.EssayCriterrions;
//using LittleSister.WriteWright.Domain.Entities;

//namespace RichGeobase.Query.Queries
//{
//    class CheckEssayByTitleAndStudentCriterion : BaseQuery, IQuery<GetEssayByTitleAndStudentCriterion, int>
//    {
//        public int Execute(GetEssayByTitleAndStudentCriterion criterion)
//        {
//            return QueryProvider.Query<Essay>()
//                .Count(t => t.Title == criterion.TitleEssay
//                    && t.Student.Email == criterion.EmailStudent
//                    && t.UserId == criterion.UserId
//                    && t.Student.Name == criterion.NameStudent);
//        }
//    }
//}
