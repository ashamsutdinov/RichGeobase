using RichGeobase.Query.Interface;

namespace RichGeobase.Query.Criterions
{
    public class GetByIdCriterion : ICriterion
    {
        public GetByIdCriterion(int id)
        {
            Id = id;
        }
        
        public int Id { get; private set; }
    }
}