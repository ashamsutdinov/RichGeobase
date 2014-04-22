using JetBrains.Annotations;

namespace RichGeobase.Query.Interface
{
    public interface IQuery<in TCriterion, out TResult>
        where TCriterion : ICriterion
    {
        [NotNull]
        TResult Execute([NotNull] TCriterion criterion);
    }
}