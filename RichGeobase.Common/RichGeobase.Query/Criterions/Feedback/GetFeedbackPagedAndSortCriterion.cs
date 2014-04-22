//namespace RichGeobase.Query.Criterions.Feedback
//{
//    public class GetFeedbackPagedAndSortCriterion : BaseGridCriterion<Entities.FeedbackMessage>
//    {
//        public bool IsArchived { get; set; }

//        private GetFeedbackPagedAndSortCriterion()
//        { }

//        public static GetFeedbackPagedAndSortCriterion Create(int pageNumber, int rowsPerPage, string sortColumn, bool isAscending, bool isArchived)
//        {
//            return new GetFeedbackPagedAndSortCriterion
//                {
//                    PageNumber = pageNumber,
//                    RowsPerPage = rowsPerPage,
//                    SortColumn = sortColumn,
//                    IsAscending = isAscending,
//                    IsArchived = isArchived,
//                };
//        }
//    }
//}