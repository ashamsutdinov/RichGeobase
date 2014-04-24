using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeoLib.Dal.Model.Entities
{
    public class BoundingBox
    {
        [Key]
        public int Id { get; set; }

        [Index]
        public float? West { get; set; }

        [Index]
        public float? North { get; set; }

        [Index]
        public float? East { get; set; }

        [Index]
        public float? South { get; set; }
    }
}