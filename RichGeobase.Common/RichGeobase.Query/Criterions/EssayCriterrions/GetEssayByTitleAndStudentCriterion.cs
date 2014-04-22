using RichGeobase.Query.Interface;

namespace RichGeobase.Query.Criterions.EssayCriterrions
{
    public class GetEssayByTitleAndStudentCriterion : ICriterion
    {
        public string EmailStudent { get;set; }
        public string TitleEssay { get; set; }
        public string NameStudent { get; set; }
        public int UserId { get; set; }
    }
}
