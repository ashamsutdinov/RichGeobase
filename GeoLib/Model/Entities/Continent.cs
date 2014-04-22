using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeoLib.Model.Entities
{
    public class Continent
    {
        [Key]
        public string Id { get; set; }

        public int? ToponymId { get; set; }

        [ForeignKey("ToponymId")]
        public Toponym Toponym { get; set; }

        public string Name { get; set; }
    }
}