namespace RichGeobase.Query.Criterions.PaymentCriterions
{
    public enum PaymentSortColumn
    {
        UserEmail,
        PaymentDate,
        PaymentStatus,
        /// <summary>
        /// A default value is <see cref="PaymentDate"/>
        /// </summary>
        Default = PaymentDate
    }
}
