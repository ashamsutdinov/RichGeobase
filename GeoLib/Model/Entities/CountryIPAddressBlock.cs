using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeoLib.Dal.Model.Entities
{
    public class CountryIPAddressBlock
    {
        [Key, Column(Order = 1)]
        public int Start { get; set; }

        [Key, Column(Order = 2)]
        public int End { get; set; }

        public int CountryId { get; set; }

        [ForeignKey("CountryId")]
        public Country Country { get; set; }
    }
}