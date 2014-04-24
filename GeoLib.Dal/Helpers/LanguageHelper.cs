using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using GeoLib.Dal.Model;
using GeoLib.Dal.Model.Entities;
using GeoLib.Helpers;

namespace GeoLib.Dal.Helpers
{
    public static class LanguageHelper
    {
        public static Language FindLanguage(this DbSet<Language> dbset, string lang)
        {
            var firstPart = lang.Split(new[] {'-'}, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault();
            lang = firstPart;
            var foundLanguage = dbset.FirstOrDefault(l => l.Id == lang || l.IsoVariant1 == lang || l.IsoVariant2 == lang);
            return foundLanguage;
        }

        public static void ParseLanguages(string path)
        {
            using (var ctx = new GeoContext())
            {
                var stream = ResourceHelper.ReadFileContent(path, true);
                using (var sr = new StreamReader(stream, Encoding.UTF8))
                {
                    while (!sr.EndOfStream)
                    {
                        var ln = sr.ReadLine();
                        Console.WriteLine(ln);
                        if (ln == null)
                            continue;

                        var parts = ln.Split(new[] { '\t' });
                        if (parts.Length < 4)
                            continue;

                        var iso3 = parts[0];
                        var iso2 = parts[1];
                        if (string.IsNullOrEmpty(iso2))
                            iso2 = null;
                        var iso1 = parts[2];
                        if (string.IsNullOrEmpty(iso1))
                            iso1 = null;
                        var descr = parts[3];
                        var id = iso3 ?? iso2 ?? iso1;

                        var lang = ctx.Languages.GetOrCreate(id);
                        lang.Entity.Id = id;
                        lang.Entity.IsoVariant1 = iso2;
                        lang.Entity.IsoVariant2 = iso1;
                        lang.Entity.Description = descr;
                        ctx.Languages.PrepareToSave(lang);
                    }
                }
                ctx.SaveChanges();
            }
        }
    }
}