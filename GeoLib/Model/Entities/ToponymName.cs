using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeoLib.Model.Entities
{
    public class ToponymName
    {
        [Key, Column(Order = 1)]
        public int ToponymId { get; set; }

        [Key, Column(Order = 2)]
        public string LanguageId { get; set; }

        [ForeignKey("ToponymId")]
        public Toponym Toponym { get; set; }

        [ForeignKey("LanguageId")]
        public Language Language { get; set; }

        [Index, StringLength(256)]
        public string Name { get; set; }
    }
}