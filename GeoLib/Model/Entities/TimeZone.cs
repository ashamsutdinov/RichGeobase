using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeoLib.Model.Entities
{
    public class TimeZone
    {
        [Key]
        public string Id { get; set; }

        [Index, StringLength(32)]
        public string Name { get; set; }

        public double GmtOffset { get; set; }

        public double DstOffset { get; set; }

        public double RawOffset { get; set; }
    }
}