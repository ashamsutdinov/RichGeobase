using System;
using System.Data.Entity;
using System.Linq;
using GeoLib.Dal.Model.Entities;

namespace GeoLib.Dal.Extensions
{
    public static class LanguagesDbSetExtensions
    {
        public static Language FindLanguage(this DbSet<Language> dbset, string lang)
        {
            var firstPart = lang.Split(new[] { '-' }, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault();
            lang = firstPart;
            var foundLanguage = dbset.FirstOrDefault(l => l.Id == lang || l.IsoVariant1 == lang || l.IsoVariant2 == lang);
            return foundLanguage;
        }
    }
}
