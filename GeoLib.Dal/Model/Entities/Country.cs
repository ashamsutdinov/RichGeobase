using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeoLib.Dal.Model.Entities
{
    public class Country
    {
        public Country()
        {
            Languages = new HashSet<Language>();
            Toponyms = new HashSet<Toponym>();
            Neighbors = new HashSet<Country>();
            NeighborTo = new HashSet<Country>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [ForeignKey("Id")]
        public Toponym Toponym { get; set; }

        public string ContinentId { get; set; }

        [ForeignKey("ContinentId")]
        public Continent Continent { get; set; }

        [Index, StringLength(16)]
        public string Code { get; set; }

        [Index, StringLength(128)]
        public string Name { get; set; }

        [Index]
        public int IsoNumeric { get; set; }

        [Index, StringLength(16)]
        public string IsoAlpha { get; set; }

        [Index, StringLength(16)]
        public string Fips { get; set; }
        
        public City CapitalCity { get; set; }
        
        [Index]
        public int Population { get; set; }

        [Index]
        public double Area { get; set; }

        public string CurrencyId { get; set; }

        [ForeignKey("CurrencyId")]
        public Currency Currency { get; set; }

        [Index, StringLength(16)]
        public string Domain { get; set; }

        public string PhoneCode { get; set; }

        public string PostalCodeFormat { get; set; }

        public string PostalCodeRegex { get; set; }

        public ICollection<Language> Languages { get; set; }

        public ICollection<Toponym> Toponyms { get; set; }

        public ICollection<Country> Neighbors { get; set; }

        public ICollection<Country> NeighborTo { get; set; }

        public ICollection<City> Cities { get; set; } 

    }
}