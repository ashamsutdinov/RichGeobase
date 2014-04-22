namespace RichGeobase.Query.Criterions.Feedback
{
    public enum FeedbackSortColumn
    {
        Receiver,
        SendDate,
        Title,
        Message,
        /// <summary>
        /// A default value is <see cref="SendDate"/>
        /// </summary>
        Default = SendDate,
    }
}