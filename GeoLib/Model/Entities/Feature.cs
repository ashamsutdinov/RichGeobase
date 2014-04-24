using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeoLib.Dal.Model.Entities
{
    public class Feature
    {
        [Key]
        public string Id { get; set; }

        public string FeatureClassId { get; set; }

        [ForeignKey("FeatureClassId")]
        public FeatureClass FeatureClass { get; set; }

        public string Name { get; set; }
    }
}