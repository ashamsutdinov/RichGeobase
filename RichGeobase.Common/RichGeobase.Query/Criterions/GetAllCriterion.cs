using RichGeobase.Query.Interface;

namespace RichGeobase.Query.Criterions
{
    public class GetAllCriterion : ICriterion
    {
        public static readonly GetAllCriterion Value = new GetAllCriterion();

        private GetAllCriterion()
        { }
    }
}