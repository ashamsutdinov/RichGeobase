using System.ComponentModel.DataAnnotations;

namespace GeoLib.Model.Entities
{
    public class FeatureClass
    {
        [Key]
        public string Id { get; set; }

        public string Name { get; set; }
    }
}