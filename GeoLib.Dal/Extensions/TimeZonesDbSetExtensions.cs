using System.Data.Entity;
using System.Linq;
using GeoLib.Dal.Model.Entities;

namespace GeoLib.Dal.Extensions
{
    public static class TimeZonesDbSetExtensions
    {
        public static TimeZone FindTimeZone(this DbSet<TimeZone> dbset, string id)
        {
            var foundTz = dbset.FirstOrDefault(t => t.Id == id || t.Name == id);
            return foundTz;
        }
    }
}