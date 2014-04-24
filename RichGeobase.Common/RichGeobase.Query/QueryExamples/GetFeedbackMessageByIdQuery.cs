//using System.Linq;
//using JetBrains.Annotations;
//using LittleSister.WriteWright.Domain.Core;
//using LittleSister.WriteWright.Domain.Criterions;
//using LittleSister.WriteWright.Domain.Entities;

//namespace RichGeobase.Query.Queries
//{
//    [UsedImplicitly]
//    internal class GetFeedbackMessageByIdQuery : BaseQuery, ISingleSelectionQuery<GetByIdCriterion, FeedbackMessage>
//    {
//        public FeedbackMessage Execute(GetByIdCriterion context)
//        {
//            return QueryProvider.Query<FeedbackMessage>().FirstOrDefault(x => x.Id == context.Id);
//        }
//    }
//}
