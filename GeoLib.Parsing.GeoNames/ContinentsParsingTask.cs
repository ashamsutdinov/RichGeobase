using System;
using System.IO;
using System.Text;
using GeoLib.Dal.Extensions;
using GeoLib.Dal.Model;
using GeoLib.Helpers;

namespace GeoLib.Parsing.GeoNames
{
    public class ContinentsParsingTask :
        ParsingTask
    {
        public ContinentsParsingTask(string path) :
            base(new[] { path })
        {
        }

        protected override void ExecuteInternal()
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
                        if (parts.Length < 3)
                            continue;

                        var id = parts[0];
                        var name = parts[1];
                        var tid = int.Parse(parts[2]);

                        var t = ToponymsDbSetExtensions.SaveToponym(tid, null, null, ctx);
                        var c = ctx.Continents.GetOrCreate(id);
                        c.Entity.Id = id;
                        c.Entity.Toponym = t;
                        c.Entity.Name = name;

                        ctx.Continents.PrepareToSave(c);
                    }
                }
                ctx.SaveChanges();
            }
        }
    }
}