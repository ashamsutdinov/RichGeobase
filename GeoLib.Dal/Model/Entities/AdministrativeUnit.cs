﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeoLib.Dal.Model.Entities
{
    public class AdministrativeUnit
    {
        [Key]
        public int Id { get; set; }

        [Index]
        public int CountryId { get; set; }

        [ForeignKey("CountryId")]
        public Country Country { get; set; }

        [Index, StringLength(128)]
        public string Code { get; set; }

        [Index]
        public int Level { get; set; }

        [Index, StringLength(256)]
        public string Name { get; set; }

        [Index, StringLength(256)]
        public string ToponymName { get; set; }

        public int? ToponymId { get; set; }

        [ForeignKey("ToponymId")]
        public Toponym Toponym { get; set; }
    }
}