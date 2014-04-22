using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using GeoLib.Model;
using GeoLib.Model.Entities;

namespace GeoLib.Helpers
{
    public static class AdministrativeUnitHelper
    {
        public static AdministrativeUnit FindAdministrativeUnit(this DbSet<AdministrativeUnit> dbset, int countryId, string code, int level)
        {
            var found = dbset.FirstOrDefault(a => a.CountryId == countryId && a.Code == code && a.Level == level);
            return found;
        }

        public static AdministrativeUnit SaveAdministrativeUnit(Country country, string code, string name, string tname, int level, int? toponymId, GeoContext context)
        {
            var ctx = context ?? new GeoContext();

            var admUnit = FindAdministrativeUnit(ctx.AdministrativeUnits, country.Id, code, level);
            if (admUnit == null)
            {
                admUnit = new AdministrativeUnit();
                ctx.AdministrativeUnits.Add(admUnit);
            }

            admUnit.Country = country;
            admUnit.Code = code;
            if (!string.IsNullOrEmpty(name))
                admUnit.Name = name;
            if (!string.IsNullOrEmpty(tname))
                admUnit.ToponymName = tname;
            admUnit.Level = level;
            if (toponymId != null)
                admUnit.ToponymId = toponymId;

            if (context == null)
            {
                ctx.SaveChanges();
                ctx.Dispose();
            }

            return admUnit;
        }

        public static void ParseAdmin1Units(string path)
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
                            if (parts.Length < 4)
                                continue;

                            var code = parts[0];
                            var name = parts[1];
                            var ascii = parts[2];
                            var stid = parts[3];

                            if (code.Contains("."))
                            {
                                var p = code.Split(new[] { '.' });
                                var coid = p[0];
                                code = p[1];

                                if (string.IsNullOrEmpty(code))
                                    continue;

                                var ctry = ctx.Countries.GetByCode(coid);
                                if (ctry != null)
                                {
                                    var tid = int.Parse(stid);
                                    var tries = 0;
                                    var toponym = ToponymHelper.SaveToponym(tid, ctry, null, ctx, false);
                                    while (toponym == null && tries < 10)
                                    {
                                        toponym = ToponymHelper.SaveToponym(tid, ctry, null, ctx, false);
                                        Thread.Sleep(100);
                                        tries++;
                                    }
                                    SaveAdministrativeUnit(ctry, code, ascii, name, 1, toponym.Id, ctx);
                                }
                            }
                            ctx.SaveChanges();
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

        public static void ParseAdmin2Units(string path)
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
                            if (parts.Length < 4)
                                continue;

                            var code = parts[0];
                            var name = parts[1];
                            var ascii = parts[2];
                            var stid = parts[3];

                            if (code.Contains("."))
                            {
                                var p = code.Split(new[] { '.' });
                                var coid = p[0];
                                var pcode = p[1];
                                code = p[2];

                                if (string.IsNullOrEmpty(code))
                                    continue;

                                var ctry = ctx.Countries.GetByCode(coid);
                                if (ctry != null)
                                {
                                    var tid = int.Parse(stid);
                                    var tries = 0;
                                    var possibleParent = ctx.AdministrativeUnits.FindAdministrativeUnit(ctry.Id, pcode, 1);
                                    Toponym parent = null;
                                    if (possibleParent != null)
                                    {
                                        var parentId = possibleParent.ToponymId.GetValueOrDefault();
                                        parent = ctx.Toponyms.GetById(parentId);
                                    }
                                    var toponym = ToponymHelper.SaveToponym(tid, ctry, parent, ctx, false);
                                    while (toponym == null && tries < 10)
                                    {
                                        toponym = ToponymHelper.SaveToponym(tid, ctry, parent, ctx, false);
                                        Thread.Sleep(100);
                                        tries++;
                                    }
                                    SaveAdministrativeUnit(ctry, code, ascii, name, 2, toponym.Id, ctx);
                                }
                            }
                            ctx.SaveChanges();
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