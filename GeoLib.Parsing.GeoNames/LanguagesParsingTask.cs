using System;
using System.IO;
using System.Text;
using GeoLib.Dal.Extensions;
using GeoLib.Dal.Model;
using GeoLib.Helpers;

namespace GeoLib.Parsing.GeoNames
{
    public abstract class LanguagesParsingTask : 
        ParsingTask
    {
        protected LanguagesParsingTask(params string[] prms) : 
            base(prms)
        {
            using (var ctx = new GeoContext())
            {
                var stream = ResourceHelper.ReadFileContent(Path, true);
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
