using System;
using System.IO;
using System.Text;
using GeoLib.Dal.Model;
using GeoLib.Helpers;

namespace GeoLib.Dal.Helpers
{
    public static class FeatureClassHelper
    {
        public static void ParseFearureClasses(string path)
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

                        var id = parts[0];
                        var name = parts[1];
                            
                        var fClass = ctx.FeatureClasses.GetOrCreate(id);
                        fClass.Entity.Id = id;
                        fClass.Entity.Name = name;
                        ctx.FeatureClasses.PrepareToSave(fClass);
                    }
                }
                ctx.SaveChanges();
            }
        }
    }
}