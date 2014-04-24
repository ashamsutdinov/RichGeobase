using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using GeoLib.Dal.Extensions;
using GeoLib.Dal.Helpers;
using GeoLib.Dal.Model;
using GeoLib.Helpers;

namespace GeoLib.Parsing.GeoNames
{
    public class AdministrativeUnitsLevel1ParsingTask :
        ParsingTask
    {
        public AdministrativeUnitsLevel1ParsingTask(string path) :
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
                                    var exists = ctx.AdministrativeUnits.FindAdministrativeUnit(ctry.Id, code, 1);
                                    if (exists != null)
                                        continue;


                                    var tid = int.Parse(stid);
                                    var tries = 0;
                                    var toponym = ToponymHelper.SaveToponym(tid, ctry, null, ctx, false);
                                    while (toponym == null && tries < 10)
                                    {
                                        toponym = ToponymHelper.SaveToponym(tid, ctry, null, ctx, false);
                                        Thread.Sleep(100);
                                        tries++;
                                    }
                                    ctx.SaveChanges();
                                    var aUnit = AdministrativeUnitDbSetExtensions.SaveAdministrativeUnit(ctry, code, ascii, name, 1, toponym != null ? (int?)toponym.Id : null, ctx);
                                    if (toponym != null)
                                        toponym.Admin1 = aUnit;
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