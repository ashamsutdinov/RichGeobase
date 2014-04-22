﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeoLib.Model.Entities
{
    public class Toponym
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime? DateUpdated { get; set; }

        public string Name { get; set; }

        public string ToponymName { get; set; }

        public int? Population { get; set; }

        public string ContinentId { get; set; }

        //[ForeignKey("ContinentId")]
        //public Continent Continent { get; set; }

        public string FeatureClassId { get; set; }

        [ForeignKey("FeatureClassId")]
        public FeatureClass FeatureClass { get; set; }

        public string FeatureId { get; set; }

        [ForeignKey("FeatureId")]
        public Feature Feature { get; set; }

        public int? Admin1Id { get; set; }

        public int? Admin2Id { get; set; }

        public int? Admin3Id { get; set; }

        public int? Admin4Id { get; set; }

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

        public DateTime? DateSourceUpdated { get; set; }

        public bool IsCity { get; set; }
    }
}
