using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeoLib.Dal.Model.Entities
{
    public class Continent
    {
        public Continent()
        {
            
        }

        [Key]
        public string Id { get; set; }

        public int? ToponymId { get; set; }

        [ForeignKey("ToponymId")]
        public Toponym Toponym { get; set; }

        public string Name { get; set; }

        public ICollection<Toponym> Toponyms { get; set; } 
    }
}