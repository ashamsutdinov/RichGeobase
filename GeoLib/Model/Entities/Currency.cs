using System.ComponentModel.DataAnnotations;

namespace GeoLib.Model.Entities
{
    public class Currency
    {
        [Key]
        public string Id { get; set; }

        public string Name { get; set; }
    }
}