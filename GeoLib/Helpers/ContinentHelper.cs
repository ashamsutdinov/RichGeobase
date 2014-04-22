using System;
using System.Globalization;
using System.IO;
using System.Text;
using GeoLib.Model;

namespace GeoLib.Helpers
{
    public static class ContinentHelper
    {
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

                        var parts = ln.Split(new[] { '\t' });
                        if (parts.Length < 3)
                            continue;

                        var id = parts[0];
                        var name = parts[1];
                        var tid = int.Parse(parts[2]);
                        

                        
                    }
                }
                ctx.SaveChanges();
            }
        }
    }
}
