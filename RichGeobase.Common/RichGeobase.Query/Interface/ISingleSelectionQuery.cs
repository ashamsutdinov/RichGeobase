namespace RichGeobase.Query.Interface
{
    public interface ISingleSelectionQuery<in TCriterion, out TResult> : IQuery<TCriterion, TResult>
        where TCriterion : ICriterion
    { }
}