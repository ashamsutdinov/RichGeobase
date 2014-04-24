using System;
using System.Data.Entity;
using System.Linq;
using GeoLib.Dal.Model;
using GeoLib.Dal.Model.Entities;
using GeoLib.GeoNames;

namespace GeoLib.Dal.Extensions
{
    public static class ToponymsDbSetExtensions
    {
        public static Toponym FindToponym(this DbSet<Toponym> dbset, string name)
        {
            var found = dbset.FirstOrDefault(t => t.Name == name || t.ToponymName == name);
            return found;
        }

        public static Toponym SaveToponym(int id, Country country, Toponym parent, GeoContext context, bool saveAdmUnits = true)
        {
            var ctx = context ?? new GeoContext();

            var requested = GeoNamesHelper.GetToponym(id);
            if (requested == null)
                return null;
            var tEntity = ctx.Toponyms.GetOrCreate(id);
            var t = tEntity.Entity;

            if (tEntity.IsNew)
            {
                t.DateCreated = DateTime.UtcNow;
            }
            t.DateUpdated = DateTime.UtcNow;

            t.Id = requested.GeoNameId;
            t.Location = LocationsDbSetExtensions.SaveToponymLocation(requested, ctx);

            if (country != null && saveAdmUnits)
            {
                if (!string.IsNullOrEmpty(requested.Admin1Code))
                    t.Admin1 = AdministrativeUnitDbSetExtensions.SaveAdministrativeUnit(country, requested.Admin1Code, requested.Admin1Name, null, 1, null, ctx);
                if (!string.IsNullOrEmpty(requested.Admin2Code))
                    t.Admin2 = AdministrativeUnitDbSetExtensions.SaveAdministrativeUnit(country, requested.Admin2Code, requested.Admin2Name, null, 2, null, ctx);
                if (!string.IsNullOrEmpty(requested.Admin3Code))
                    t.Admin3 = AdministrativeUnitDbSetExtensions.SaveAdministrativeUnit(country, requested.Admin3Code, requested.Admin3Name, null, 3, null, ctx);
                if (!string.IsNullOrEmpty(requested.Admin4Code))
                    t.Admin4 = AdministrativeUnitDbSetExtensions.SaveAdministrativeUnit(country, requested.Admin4Code, requested.Admin4Name, null, 4, null, ctx);
            }

            t.Country = country;
            t.Parent = parent;
            if (!string.IsNullOrEmpty(requested.ContinentCode))
            {
                var continent = ctx.Continents.GetById(requested.ContinentCode);
                t.Continent = continent;
            }
            if (!string.IsNullOrEmpty(requested.FeatureClassCode))
            {
                t.FeatureClassId = requested.FeatureClassCode;
            }
            if (!string.IsNullOrEmpty(requested.FeatureCode))
            {
                t.FeatureId = requested.FeatureCode;
            }
            t.Name = requested.Name;
            t.ToponymName = requested.ToponymName;
            t.Population = requested.Population;

            var nlang = ctx.Languages.FindLanguage("_");
            if (nlang != null)
            {
                var ename = ctx.ToponymNames.GetOrCreate(t.Id, nlang.Id);
                ename.Entity.ToponymId = t.Id;
                ename.Entity.LanguageId = nlang.Id;
                ename.Entity.Name = requested.Name;
                ctx.ToponymNames.PrepareToSave(ename);
            }

            foreach (var name in requested.AlternateNames)
            {
                if (!string.IsNullOrEmpty(name.Language))
                {
                    switch (name.Language)
                    {
                        case "post":
                            t.PostalCode = name.Name;
                            break;
                        case "iata":
                            t.IATA = name.Name;
                            break;
                        case "icao":
                            t.ICAO = name.Name;
                            break;
                        case "faac":
                            t.FAAC = name.Name;
                            break;
                        default:
                            if (name.Language.Length < 4 || name.Language.Contains("-"))
                            {
                                var language = ctx.Languages.FindLanguage(name.Language);
                                if (language == null)
                                    continue;

                                var ename = ctx.ToponymNames.GetOrCreate(t.Id, language.Id);
                                ename.Entity.ToponymId = t.Id;
                                ename.Entity.LanguageId = language.Id;
                                ename.Entity.Name = name.Name;
                                ctx.ToponymNames.PrepareToSave(ename);
                            }
                            break;
                    }
                }
            }

            ctx.Toponyms.PrepareToSave(tEntity);

            if (context == null)
            {
                ctx.SaveChanges();
                ctx.Dispose();
            }

            return t;
        }
    }
}