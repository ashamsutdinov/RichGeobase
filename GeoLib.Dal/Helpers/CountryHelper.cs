using System;
using System.Data.Entity;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using GeoLib.Dal.Model;
using GeoLib.Dal.Model.Entities;
using GeoLib.Dal.Resources;
using GeoLib.Helpers;
using Currency = GeoLib.Dal.Model.Entities.Currency;

namespace GeoLib.Dal.Helpers
{
    public static class CountryHelper
    {
        public static Country GetByCode(this DbSet<Country> dbset, string code)
        {
            var found = dbset.FirstOrDefault(c => c.Code == code);
            return found;
        }

        public static void FillCapitalCities(string path)
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

                            var capital = parts[5];
                            var sid = parts[16];
                            if (string.IsNullOrEmpty(sid))
                                continue;
                            var id = int.Parse(sid);
                            var country = ctx.Countries.GetById(id);
                            if (country != null)
                            {
                                var possibleCapitalCity = ctx.Toponyms.FindToponym(capital);
                                if (possibleCapitalCity != null && possibleCapitalCity.CountryId == id)
                                {
                                    var city = ctx.Cities.GetById(possibleCapitalCity.Id);
                                    country.CapitalCity = city;
                                }

                                ctx.SaveChanges();
                            }
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

        public static void ParseCountries(string path, bool forsed = false, bool saveToponym = true)
        {
            var stream = ResourceHelper.ReadFileContent(path);
            using (var ctx = new GeoContext())
            {
                using (var sr = new StreamReader(stream, Encoding.UTF8))
                {
                    while (!sr.EndOfStream)
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

                        var iso = parts[0];
                        var iso3 = parts[1];
                        var sison = parts[2];
                        var ison = int.Parse(sison);
                        var fips = parts[3];
                        var cn = parts[4];
                        //var capital = parts[5];
                        var sarea = parts[6];
                        var area = 0.0;
                        if (!string.IsNullOrEmpty(sarea))
                        {
                            area = double.Parse(sarea, CultureInfo.InvariantCulture);
                        }
                        var spop = parts[7];
                        var pop = int.Parse(spop);
                        var ctn = parts[8];
                        if (string.IsNullOrEmpty(ctn))
                            ctn = null;
                        var domain = parts[9];
                        if (string.IsNullOrEmpty(domain))
                            domain = null;
                        var currc = parts[10];
                        if (string.IsNullOrEmpty(currc))
                            currc = null;
                        var curr = parts[11];
                        var phone = parts[12];
                        if (string.IsNullOrEmpty(phone))
                            phone = null;
                        var pformat = parts[13];
                        if (string.IsNullOrEmpty(pformat))
                            pformat = null;
                        var pregex = parts[14];
                        if (string.IsNullOrEmpty(pregex))
                            pregex = null;
                        var langs = parts[15];
                        var sid = parts[16];
                        if (string.IsNullOrEmpty(sid))
                            continue;
                        var id = int.Parse(sid);
                        var nbrs = parts[17].Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        //var eqfips = parts[18];

                        var country = ctx.Countries.GetOrCreate(id);
                        if (!country.IsNew && !forsed)
                        {
                            var t = ctx.Toponyms.GetById(country.Entity.Id);
                            if (t != null && t.DateUpdated.GetValueOrDefault().AddDays(7) > DateTime.UtcNow)
                            {
                                Console.WriteLine(GeoRes.CountryHelper_ParseCountries_Skipping_country___0_, t.Name);
                                continue;
                            }
                        }

                        Currency currency = null;
                        if (!string.IsNullOrEmpty(currc))
                        {
                            currency = CurrencyHelper.SaveCurrency(currc, curr, ctx);
                        }
                        var c = country.Entity;

                        if (saveToponym)
                        {
                            var tries = 0;
                            var toponym = ToponymHelper.SaveToponym(id, null, null, ctx);
                            while (toponym == null && tries < 10)
                            {
                                toponym = ToponymHelper.SaveToponym(id, null, null, ctx);
                                Thread.Sleep(100);
                                tries++;
                            }
                            c.Toponym = toponym;
                        }

                        c.Id = id;
                        c.Name = cn;
                        c.Code = iso;
                        c.IsoAlpha = iso3;
                        c.IsoNumeric = ison;
                        c.Fips = fips;
                        c.Area = area;
                        c.Population = pop;
                        c.Domain = domain;
                        c.PhoneCode = phone;
                        c.PostalCodeFormat = pformat;
                        c.PostalCodeRegex = pregex;
                        c.Currency = currency;
                        c.ContinentId = ctn;
                        var alangs = string.Format("{0}", langs).Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        if (!country.IsNew)
                        {
                            ctx.Entry(c).Collection(ct => ct.Languages).Load();
                            ctx.Entry(c).Collection(ct => ct.Neighbors).Load();
                        }
                        foreach (var nbr in nbrs)
                        {
                            if (c.Neighbors.Any(ct => ct.Code == nbr))
                                continue;
                            var nctrry = GetByCode(ctx.Countries, nbr);
                            if (nctrry != null)
                            {
                                c.Neighbors.Add(nctrry);
                            }
                        }
                        foreach (var alang in alangs)
                        {
                            var lang = ctx.Languages.FindLanguage(alang);
                            if (lang != null && c.Languages != null)
                            {
                                if (c.Languages.Any(l => l.Id == lang.Id))
                                    continue;
                                c.Languages.Add(lang);
                            }
                            else
                            {
                                Console.WriteLine(GeoRes.CountryHelper_ParseCountries_Undefined_language__0_, alang);
                            }
                        }
                        ctx.Countries.PrepareToSave(country);
                        ctx.SaveChanges();
                    }
                }
            }
        }
    }
}