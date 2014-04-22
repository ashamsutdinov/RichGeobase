using System;
using System.IO;
using System.Text;
using GeoLib.Model;

namespace GeoLib.Helpers
{
    public static class FeatureHelper
    {
        public static void ParseFeature(string path, string lang)
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
                        if (parts.Length < 2)
                            continue;

                        var ids = parts[0];
                        var name = parts[1];
                        var idparts = ids.Split(new[] {'.'}, StringSplitOptions.RemoveEmptyEntries);
                        if (idparts.Length != 2)
                            continue;
                        var fcid = idparts[0];
                        var id = idparts[1];

                        var feature = ctx.Features.GetOrCreate(id);
                        feature.Entity.Id = id;
                        feature.Entity.FeatureClassId = fcid;
                        if (feature.IsNew)
                        {
                            feature.Entity.Name = name;
                        }
                        ctx.Features.PrepareToSave(feature);

                        if (!string.IsNullOrEmpty(lang))
                        {
                            var elang = ctx.Languages.FindLanguage(lang);
                            if (elang != null)
                            {
                                var fname = ctx.FeatureNames.GetOrCreate(id, elang.Id);
                                fname.Entity.Name = name;
                                if (parts.Length == 3)
                                {
                                    var desc = parts[2];
                                    fname.Entity.Feature = feature.Entity;
                                    fname.Entity.Language = elang;
                                    fname.Entity.Name = name;
                                    fname.Entity.Description = desc;
                                }
                                ctx.FeatureNames.PrepareToSave(fname);
                            }
                        }
                    }
                }
                ctx.SaveChanges();
            }
        }
    }
}