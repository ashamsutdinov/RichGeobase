using System.Data.Entity;
using System.Linq;
using GeoLib.Dal.Model.Entities;

namespace GeoLib.Dal.Extensions
{
    public static class CountriesDbSetExtensions
    {
        public static Country GetByCode(this DbSet<Country> dbset, string code)
        {
            var found = dbset.FirstOrDefault(c => c.Code == code);
            return found;
        }
    }
}