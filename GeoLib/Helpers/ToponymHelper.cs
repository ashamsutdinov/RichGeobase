using System;
using GeoLib.GeoNames;
using GeoLib.Model;
using GeoLib.Model.Entities;

namespace GeoLib.Helpers
{
    public static class ToponymHelper
    {
        public static Toponym SaveToponym(int id, Country country, Toponym parent, GeoContext context)
        {
            var ctx = context ?? new GeoContext();

            var requested = GeoNamesHelper.GetToponym(id);
            var tEntity = ctx.Toponyms.GetOrCreate(id);
            var t = tEntity.Entity;

            if (tEntity.IsNew)
            {
                t.DateCreated = DateTime.UtcNow;
            }
            t.DateUpdated = DateTime.UtcNow;

            t.Id = requested.GeoNameId;
            t.Location = LocationHelper.SaveToponymLocation(requested, ctx);

            if (country != null)
            {
                t.Admin1 = AdministrativeUnitHelper.SaveAdministrativeUnit(country, requested.Admin1Code, requested.Admin1Name, null, 1, null, ctx);
                t.Admin2 = AdministrativeUnitHelper.SaveAdministrativeUnit(country, requested.Admin2Code, requested.Admin2Name, null, 2, null, ctx);
                t.Admin3 = AdministrativeUnitHelper.SaveAdministrativeUnit(country, requested.Admin3Code, requested.Admin3Name, null, 3, null, ctx);
                t.Admin4 = AdministrativeUnitHelper.SaveAdministrativeUnit(country, requested.Admin4Code, requested.Admin4Name, null, 4, null, ctx);
            }

            t.Country = country;
            t.Parent = parent;
            if (!string.IsNullOrEmpty(requested.ContinentCode))
            {
                t.ContinentId = requested.ContinentCode;
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

            if (context == null)
            {
                ctx.SaveChanges();
                ctx.Dispose();
            }

            ctx.Toponyms.PrepareToSave(tEntity);

            return t;
        }
    }
}