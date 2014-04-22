using System.ComponentModel.DataAnnotations;

namespace GeoLib.Model.Entities
{
    public class Language
    {
        [Key]
        public string Id { get; set; }

        public string Iso3 { get; set; }

        public string Iso2 { get; set; }

        public string Iso1 { get; set; }

        public string Description { get; set; }
    }
}