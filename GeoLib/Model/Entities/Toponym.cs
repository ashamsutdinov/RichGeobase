using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeoLib.Model.Entities
{
    public class Toponym
    {
        public Toponym()
        {
            IsCapitalCityFor = new HashSet<Country>();    
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime DateCreated { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? DateUpdated { get; set; }

        [Index]
        [StringLength(256)]
        public string Name { get; set; }

        [Index]
        [StringLength(256)]
        public string ToponymName { get; set; }

        public long? Population { get; set; }

        //[ForeignKey("ContinentId")]
        public Continent Continent { get; set; }

        [Index]
        [StringLength(8)]
        public string ContinentId { get; set; }

        public int? CountryId { get; set; }

        //[ForeignKey("CountryId")]
        public Country Country { get; set; }

        public ICollection<Country> IsCapitalCityFor { get; set; } 

        public string FeatureClassId { get; set; }

        [ForeignKey("FeatureClassId")]
        public FeatureClass FeatureClass { get; set; }

        public string FeatureId { get; set; }

        [ForeignKey("FeatureId")]
        public Feature Feature { get; set; }

        public int? Admin1Id { get; set; }

        [ForeignKey("Admin1Id")]
        public AdministrativeUnit Admin1 { get; set; }

        public int? Admin2Id { get; set; }

        [ForeignKey("Admin2Id")]
        public AdministrativeUnit Admin2 { get; set; }

        public int? Admin3Id { get; set; }

        [ForeignKey("Admin3Id")]
        public AdministrativeUnit Admin3 { get; set; }

        public int? Admin4Id { get; set; }

        [ForeignKey("Admin4Id")]
        public AdministrativeUnit Admin4 { get; set; }

        public string TimeZoneId { get; set; }

        [ForeignKey("TimeZoneId")]
        public TimeZone TimeZone { get; set; }

        public int? ParentId { get; set; }

        [ForeignKey("ParentId")]
        public Toponym Parent { get; set; }

        public int? LocationId { get; set; }

        [ForeignKey("LocationId")]
        public Location Location { get; set; }

        public int? BoundingBoxId { get; set; }

        [ForeignKey("BoundingBoxId")]
        public BoundingBox BoundingBox { get; set; }

        public string PhoneCode { get; set; }

        public string PostalCode { get; set; }

        public string IATA { get; set; }

        public string ICAO { get; set; }

        public string FAAC { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? DateSourceUpdated { get; set; }

        public bool IsCity { get; set; }
    }
}
