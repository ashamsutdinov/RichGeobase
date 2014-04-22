using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeoLib.Model.Entities
{
    public class Language
    {
        public Language()
        {
            Countries = new HashSet<Country>();
        }

        [Key]
        public string Id { get; set; }

        [Index]
        [StringLength(16)]
        public string IsoVariant1 { get; set; }

        [Index]
        [StringLength(16)]
        public string IsoVariant2 { get; set; }

        public string Description { get; set; }

        public ICollection<Country> Countries { get; set; }
    }
}