using System;
using System.Data.Entity;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using GeoLib.GeoNames;
using GeoLib.Model;
using GeoLib.Model.Entities;

namespace GeoLib.Helpers
{
    public static class ToponymHelper
    {
        public static Toponym FindToponym(this DbSet<Toponym> dbset, string name)
        {
            var found = dbset.FirstOrDefault(t => t.Name == name || t.ToponymName == name);
            return found;
        }

        public static void ParseToponymNames(string path)
        {
            var stream = ResourceHelper.ReadFileContent(path);
            using (var ctx = new GeoContext())
            {
                using (var sr = new StreamReader(stream, Encoding.UTF8))
                {
                    while (!sr.EndOfStream)
                    {
                        try
                        {
                            var line = sr.ReadLine();
                            if (line == null)
                                continue;

                            if (line.StartsWith("#"))
                                continue;

                            Console.WriteLine(line);

                            var parts = line.Split(new[] {'\t'});
                            if (parts.Length < 4)
                                continue;

                            var sid = parts[1];
                            if (string.IsNullOrEmpty(sid))
                                continue;

                            var id = int.Parse(sid);
                            var t = ctx.Toponyms.GetById(id);
                            if (t == null)
                                continue;

                            var lang = parts[2];
                            var value = parts[3];

                            switch (lang)
                            {
                                case "post":
                                    t.PostalCode = value;
                                    break;
                                case "iata":
                                    t.IATA = value;
                                    break;
                                case "icao":
                                    t.ICAO = value;
                                    break;
                                case "faac":
                                    t.FAAC = value;
                                    break;
                                default:
                                    if (lang.Length < 4 || lang.Contains("-") || lang.Contains("/"))
                                    {
                                        var language = ctx.Languages.FindLanguage(lang);
                                        if (language == null)
                                            continue;

                                        var ename = ctx.ToponymNames.GetOrCreate(t.Id, language.Id);
                                        ename.Entity.ToponymId = t.Id;
                                        ename.Entity.LanguageId = language.Id;
                                        ename.Entity.Name = value;
                                        ctx.ToponymNames.PrepareToSave(ename);
                                    }
                                    break;
                            }
                            
                            ctx.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.ToString());
                            Console.ReadKey();
                        }
                    }
                }
            }
        }

        public static void ParseToponyms(string path)
        {
            var stream = ResourceHelper.ReadFileContent(path);
            using (var ctx = new GeoContext())
            {
                using (var sr = new StreamReader(stream, Encoding.UTF8))
                {
                    while (!sr.EndOfStream)
                    {
                        try
                        {
                            var line = sr.ReadLine();
                            if (line == null)
                                continue;

                            if (line.StartsWith("#"))
                                continue;

                            Console.WriteLine(line);

                            var parts = line.Split(new[] { '\t' });
                            if (parts.Length < 19)
                                continue;

                            var sid = parts[0];
                            if (string.IsNullOrEmpty(sid))
                                continue;

                            var id = int.Parse(sid);
                            if (id <= 0)
                                continue;

                            var toponym = ctx.Toponyms.GetOrCreate(id);
                            var t = toponym.Entity;

                            t.Id = id;

                            var tname = parts[1];
                            var name = parts[2];
                            t.ToponymName = tname;
                            t.Name = name;

                            var nlang = ctx.Languages.FindLanguage("_");
                            if (nlang != null)
                            {
                                var ename = ctx.ToponymNames.GetOrCreate(t.Id, nlang.Id);
                                ename.Entity.ToponymId = t.Id;
                                ename.Entity.LanguageId = nlang.Id;
                                ename.Entity.Name = name;
                                ctx.ToponymNames.PrepareToSave(ename);
                            }

                            var slat = parts[4];
                            var slng = parts[5];
                            var sele = parts[15];

                            if (!string.IsNullOrEmpty(slat) && !string.IsNullOrEmpty(slng))
                            {
                                var lat = double.Parse(slat, CultureInfo.InvariantCulture);
                                var lng = double.Parse(slng, CultureInfo.InvariantCulture);
                                int? ele = null;
                                if (!string.IsNullOrEmpty(sele))
                                {
                                    ele = int.Parse(sele);
                                }
                                var location = LocationHelper.SaveToponymLocation(lat, lng, ele, ctx);
                                t.Location = location;
                            }

                            var fclass = parts[6];
                            if (!string.IsNullOrEmpty(fclass))
                            {
                                t.FeatureClassId = fclass;
                            }

                            var f = parts[7];
                            if (!string.IsNullOrEmpty(f))
                            {
                                t.FeatureId = f;
                            }
                           
                            Country ctry = null;
                            var ccode = parts[8];
                            if (!string.IsNullOrEmpty(ccode))
                            {
                                ctry = ctx.Countries.GetByCode(ccode);
                                if (ctry != null)
                                {
                                    t.Country = ctry;
                                }
                            }

                            //var cc2 = parts[9];

                            #region AdmUnits

                            if (ctry != null)
                            {
                                var adm1Code = parts[10];
                                if (!string.IsNullOrEmpty(adm1Code))
                                {
                                    var aUnit = AdministrativeUnitHelper.SaveAdministrativeUnit(ctry, adm1Code, null, null, 1, null, ctx);
                                    t.Admin1 = aUnit;
                                }
                                var adm2Code = parts[11];
                                if (!string.IsNullOrEmpty(adm2Code))
                                {
                                    var aUnit = AdministrativeUnitHelper.SaveAdministrativeUnit(ctry, adm2Code, null, null, 2, null, ctx);
                                    t.Admin2 = aUnit;
                                }
                                var adm3Code = parts[12];
                                if (!string.IsNullOrEmpty(adm3Code))
                                {
                                    var aUnit = AdministrativeUnitHelper.SaveAdministrativeUnit(ctry, adm3Code, null, null, 3, null, ctx);
                                    t.Admin3 = aUnit;
                                }

                                var adm4Code = parts[13];
                                if (!string.IsNullOrEmpty(adm4Code))
                                {
                                    var aUnit = AdministrativeUnitHelper.SaveAdministrativeUnit(ctry, adm3Code, null, null, 4, null, ctx);
                                    t.Admin4 = aUnit;
                                }
                                var firstExistingAdmUnit = t.Admin4 ?? t.Admin3 ?? t.Admin2 ?? t.Admin1;
                                if (firstExistingAdmUnit != null && firstExistingAdmUnit.ToponymId != null)
                                {
                                    t.ParentId = firstExistingAdmUnit.ToponymId;
                                }
                                else
                                {
                                    firstExistingAdmUnit = t.Admin3 ?? t.Admin2 ?? t.Admin1;
                                    if (firstExistingAdmUnit != null && firstExistingAdmUnit.ToponymId != null)
                                    {
                                        t.ParentId = firstExistingAdmUnit.ToponymId;
                                    }
                                    else
                                    {
                                        firstExistingAdmUnit = t.Admin2 ?? t.Admin1;
                                        if (firstExistingAdmUnit != null && firstExistingAdmUnit.ToponymId != null)
                                        {
                                            t.ParentId = firstExistingAdmUnit.ToponymId;
                                        }
                                        else
                                        {
                                            firstExistingAdmUnit = t.Admin1;
                                            if (firstExistingAdmUnit != null && firstExistingAdmUnit.ToponymId != null)
                                            {
                                                t.ParentId = firstExistingAdmUnit.ToponymId;
                                            }
                                        }
                                    }
                                }
                            }

                            #endregion

                            var spop = parts[14];
                            if (!string.IsNullOrEmpty(spop))
                            {
                                var pop = int.Parse(spop);
                                t.Population = pop;
                            }

                            //var sele = parts[15];

                            //var dem = parts[16];

                            var tzone = parts[17];
                            if (!string.IsNullOrEmpty(tzone))
                            {
                                t.TimeZoneId = tzone;
                            }

                            var mdate = parts[18];
                            if (!string.IsNullOrEmpty(mdate))
                            {
                                var pdate = mdate.Split(new[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
                                if (pdate.Length == 3)
                                {
                                    var year = int.Parse(pdate[0]);
                                    var month = int.Parse(pdate[1]);
                                    var day = int.Parse(pdate[2]);
                                    var date = new DateTime(year, month, day);
                                    t.DateSourceUpdated = date;
                                }
                            }

                            ctx.Toponyms.PrepareToSave(toponym);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.ToString());
                            Console.ReadKey();
                        }
                        ctx.SaveChanges();
                    }
                }
            }
        }

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