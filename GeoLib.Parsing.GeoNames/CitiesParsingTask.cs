using System;
using System.IO;
using System.Text;
using GeoLib.Dal.Extensions;
using GeoLib.Dal.Model;
using GeoLib.Dal.Model.Entities;
using GeoLib.Helpers;

namespace GeoLib.Parsing.GeoNames
{
    public class CitiesParsingTask :
        ParsingTask
    {
        protected CitySize Size { get; private set; }

        public CitiesParsingTask(string path, CitySize size) : 
            base(new object[]{path, size})
        {
            Size = size;
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

                            var sid = parts[0];
                            if (string.IsNullOrEmpty(sid))
                                continue;

                            var id = int.Parse(sid);

                            var ccode = parts[8];
                            if (!string.IsNullOrEmpty(ccode))
                            {
                                var ctry = ctx.Countries.GetByCode(ccode);
                                if (ctry != null)
                                {
                                    var city = ctx.Cities.GetOrCreate(id);
                                    city.Entity.Id = id;
                                    city.Entity.Country = ctry;
                                    city.Entity.Size = Size;
                                    ctx.Cities.PrepareToSave(city);
                                }
                            }
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
    }
}