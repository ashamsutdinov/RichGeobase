using RichGeobase.Query.Interface;

namespace RichGeobase.Query.Criterions.InfoBlock
{
    public class GetInfoBlocksByPageIdCriterion : ICriterion
    {
        public GetInfoBlocksByPageIdCriterion(int  pageId)
        {
            PageId = pageId;
        }

        public int PageId { get; private set; }
    }
}
