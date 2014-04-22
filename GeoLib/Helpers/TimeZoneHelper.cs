using System;
using System.Data.Entity;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using GeoLib.Model;

namespace GeoLib.Helpers
{
    public static class TimeZoneHelper
    {
        public static Model.Entities.TimeZone FindTimeZone(this DbSet<Model.Entities.TimeZone> dbset, string id)
        {
            var foundTz = dbset.FirstOrDefault(t => t.Id == id || t.Name == id);
            return foundTz;
        }

        public static void ParseFeature(string path)
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

                        var parts = ln.Split(new[] {'\t'});
                        if (parts.Length < 5)
                            continue;

                        var id = parts[0];
                        var name = parts[1];
                        var sgmt = parts[2];
                        var gmt = double.Parse(sgmt, CultureInfo.InvariantCulture);
                        var sdst = parts[3];
                        var dst = double.Parse(sdst, CultureInfo.InvariantCulture);
                        var sraw = parts[4];
                        var raw = double.Parse(sraw, CultureInfo.InvariantCulture);

                        var timeZone = ctx.TimeZones.GetOrCreate(id);
                        timeZone.Entity.Id = id;
                        timeZone.Entity.Name = name;
                        timeZone.Entity.GmtOffset = gmt;
                        timeZone.Entity.DstOffset = dst;
                        timeZone.Entity.RawOffset = raw;
                        ctx.TimeZones.PrepareToSave(timeZone);
                    }
                }
                ctx.SaveChanges();
            }
        }
    }
}