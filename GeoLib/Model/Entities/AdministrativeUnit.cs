using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeoLib.Model.Entities
{
    public class AdministrativeUnit
    {
        [Key]
        public int Id { get; set; }

        public int CountryId { get; set; }

        [ForeignKey("CountryId")]
        public Country Country { get; set; }

        [Index]
        [StringLength(32)]
        public string Code { get; set; }

        [Index]
        public int Level { get; set; }

        [Index]
        [StringLength(256)]
        public string Name { get; set; }

        [Index]
        [StringLength(256)]
        public string ToponymName { get; set; }

        public int ToponymId { get; set; }

        [ForeignKey("ToponymId")]
        public Toponym Toponym { get; set; }
    }
}