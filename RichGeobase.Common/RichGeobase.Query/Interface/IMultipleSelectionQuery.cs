using System.Collections.Generic;

namespace RichGeobase.Query.Interface
{
    public interface IMultipleSelectionQuery<in TCriterion, out TResult> : IQuery<TCriterion, IEnumerable<TResult>>
        where TCriterion : ICriterion
    { }
}