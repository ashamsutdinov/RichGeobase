namespace RichGeobase.Query.Criterions
{
    public abstract class BasePagedAndSortCriterion<TSortColumn>
    {
        public int PageNumber { get; set; }
        public int RowsPerPage { get; set; }
        public TSortColumn SortColumn { get; set; }
        public bool IsAscending { get; set; }
    }
}
