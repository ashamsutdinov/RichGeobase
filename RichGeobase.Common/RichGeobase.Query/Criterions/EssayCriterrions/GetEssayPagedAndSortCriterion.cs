//using RichGeobase.Query.Interface;

//namespace RichGeobase.Query.Criterions.EssayCriterrions
//{
//    public class GetEssayPagedAndSortCriterion: BaseGridCriterion<Essay>, ICriterion
//    {
//        public bool IsDraft { get; set; }
//        public int UserId { get; set; }
        
//        private GetEssayPagedAndSortCriterion()
//        { }

//        public static GetEssayPagedAndSortCriterion Create(int pageNumber, int rowsPerPage, string sortColumn, bool isAscending, bool isDraft)
//        {
//            return new GetEssayPagedAndSortCriterion
//            {
//                PageNumber = pageNumber,
//                RowsPerPage = rowsPerPage,
//                SortColumn = sortColumn,
//                IsAscending = isAscending,
//                IsDraft = isDraft
//            };
//        }
//    }
//}
