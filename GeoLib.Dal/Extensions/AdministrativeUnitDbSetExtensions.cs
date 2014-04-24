using System.Data.Entity;
using System.Linq;
using GeoLib.Dal.Model;
using GeoLib.Dal.Model.Entities;

namespace GeoLib.Dal.Extensions
{
    public static class AdministrativeUnitDbSetExtensions
    {
        public static AdministrativeUnit FindAdministrativeUnit(this DbSet<AdministrativeUnit> dbset, int countryId, string code, int level)
        {
            var found = dbset.FirstOrDefault(a => a.CountryId == countryId && a.Code == code && a.Level == level);
            return found;
        }

        public static AdministrativeUnit SaveAdministrativeUnit(Country country, string code, string name, string tname, int level, int? toponymId, GeoContext context)
        {
            var ctx = context ?? new GeoContext();

            var admUnit = FindAdministrativeUnit(ctx.AdministrativeUnits, country.Id, code, level);
            if (admUnit == null)
            {
                admUnit = new AdministrativeUnit();
                ctx.AdministrativeUnits.Add(admUnit);
            }

            admUnit.Country = country;
            admUnit.Code = code;
            if (!string.IsNullOrEmpty(name))
                admUnit.Name = name;
            if (!string.IsNullOrEmpty(tname))
                admUnit.ToponymName = tname;
            admUnit.Level = level;
            if (toponymId != null)
                admUnit.ToponymId = toponymId;

            if (context == null)
            {
                ctx.SaveChanges();
                ctx.Dispose();
            }

            return admUnit;
        }
    }
}