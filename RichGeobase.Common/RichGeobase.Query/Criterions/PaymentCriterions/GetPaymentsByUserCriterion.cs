using RichGeobase.Query.Interface;

namespace RichGeobase.Query.Criterions.PaymentCriterions
{
    public class GetPaymentsByUserCriterion : ICriterion
    {
        public int UserId { get; set; }
    }
}
