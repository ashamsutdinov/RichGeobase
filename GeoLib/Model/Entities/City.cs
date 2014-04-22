using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeoLib.Model.Entities
{
    public class City
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Id")]
        public Toponym Toponym { get; set; }

        public int CountryId { get; set; }
        
        public Country Country { get; set; }

        [Index, Column(TypeName = "INT")]
        public CitySize Size { get; set; }

        [Index]
        public int? CapitalCityForCountryId { get; set; }

        public Country CapitalCityForCountry { get; set; }
    }
}