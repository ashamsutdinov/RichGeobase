//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Linq.Expressions;
//using JetBrains.Annotations;
//using LittleSister.WriteWright.Domain.Core;
//using LittleSister.WriteWright.Domain.Criterions;
//using LittleSister.WriteWright.Common.Extensions;
//using LittleSister.WriteWright.Domain.Criterions.Administrator;
//using LittleSister.WriteWright.Domain.Entities;
//using LittleSister.WriteWright.Domain.ValueObjects;

//namespace RichGeobase.Query.Queries
//{
//    internal class GetEssayForIndividualUserAdminGridQuery : BaseQuery, IMultipleSelectionQuery<GetEssayWithIndividualUserCriterrion, EssayAdminGrid>
//    {
//        public IEnumerable<EssayAdminGrid> Execute(GetEssayWithIndividualUserCriterrion criterion)
//        {
//            return QueryProvider.Query<Essay>()
//                .Join(QueryProvider.Query<IndividualUser>(), essay => essay.UserId,
//                      individualUser => individualUser.Id,
//                      (essay, individualUser) => new { essay, individualUser }).
//                      OrderBy(k => k.essay.Title).
//                Page(criterion.PageNumber, criterion.RowsPerPage)
//                .Select(@t => new EssayAdminGrid()
//              {
//                  EmailTeacher = @t.individualUser.Email,
//                  EssayId = @t.essay.Id,
//                  NameTeacher = @t.individualUser.LastName + " " + @t.individualUser.FirstName,
//                  Title = @t.essay.Title,
//                  ImportDate = @t.essay.ImportDate,
//                  Status = @t.essay.Status,
//                  StudentEmail = @t.essay.Student.Email,
//                  StudentName = @t.essay.Student.Name,
//                  UserId = @t.individualUser.Id
//              });  
//        }
//    }
//}
