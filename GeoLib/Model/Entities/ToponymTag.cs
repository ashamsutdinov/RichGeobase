using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeoLib.Dal.Model.Entities
{
    public class ToponymTag
    {
        public const string KladrIdKey = "KLADR_ID";

        [Key, Column(Order = 1)]
        public int ToponymId { get; set; }

        [ForeignKey("ToponymId")]
        public Toponym Toponym { get; set; }

        [Key, Column(Order = 2)]
        public string Key { get; set; }

        [Index, StringLength(256)]
        public string Value { get; set; }
    }
}