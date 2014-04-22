using JetBrains.Annotations;
using RichGeobase.Query.Interface;

namespace RichGeobase.Query.Criterions.InfoBlock
{
    public class GetInfoBlockByReserveTypeCriterion : ICriterion
    {
        [NotNull]
        public bool IsReserved { get; set; }

        public  GetInfoBlockByReserveTypeCriterion(bool isReserved)
        {
            IsReserved = isReserved;
        }
    }
}
