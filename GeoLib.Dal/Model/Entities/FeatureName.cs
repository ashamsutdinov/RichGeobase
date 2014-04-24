using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeoLib.Dal.Model.Entities
{
    public class FeatureName
    {
        [Key, Column(Order = 1)]
        public string FeatureId { get; set; }

        [Key, Column(Order = 2)]
        public string LanguageId { get; set; }

        [ForeignKey("FeatureId")]
        public Feature Feature { get; set; }

        [ForeignKey("LanguageId")]
        public Language Language { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}