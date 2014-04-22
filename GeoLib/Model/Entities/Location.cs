using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeoLib.Model.Entities
{
    public class Location
    {
        [Key]
        public int Id { get; set; }

        [Index]
        public float? Latitude { get; set; }

        [Index]
        public float? Longitude { get; set; }

        public int? Elevation { get; set; }
    }
}