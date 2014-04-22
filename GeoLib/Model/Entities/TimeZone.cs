using System.ComponentModel.DataAnnotations;

namespace GeoLib.Model.Entities
{
    public class TimeZone
    {
        [Key]
        public string Id { get; set; }

        public int GmtOffset { get; set; }

        public int DstOffset { get; set; }

        public int RawOffset { get; set; }
    }
}