using System;
using System.IO;
using System.Text;
using GeoLib.Dal.Extensions;
using GeoLib.Dal.Model;
using GeoLib.Helpers;

namespace GeoLib.Parsing.GeoNames
{
    public class FillCapitalCitiesParsingTask :
        ParsingTask
    {
        public FillCapitalCitiesParsingTask(string path) :
            base(new[] { path })
        {
        }

        protected override void ExecuteInternal()
        {
            var stream = ResourceHelper.ReadFileContent(Path);
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
    }
}