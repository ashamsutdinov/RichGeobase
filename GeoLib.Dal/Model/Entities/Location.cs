﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeoLib.Dal.Model.Entities
{
    public class Location
    {
        [Key]
        public int Id { get; set; }

        [Index]
        public double? Latitude { get; set; }

        [Index]
        public double? Longitude { get; set; }

        public int? Elevation { get; set; }
    }
}