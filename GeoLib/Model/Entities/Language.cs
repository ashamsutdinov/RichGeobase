using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeoLib.Model.Entities
{
    public class Language
    {
        [Key]
        public string Id { get; set; }

        [Index]
        [StringLength(8)]
        public string Iso3 { get; set; }

        [Index]
        [StringLength(8)]
        public string Iso2 { get; set; }

        [Index]
        [StringLength(8)]
        public string Iso1 { get; set; }

        public string Description { get; set; }
    }
}