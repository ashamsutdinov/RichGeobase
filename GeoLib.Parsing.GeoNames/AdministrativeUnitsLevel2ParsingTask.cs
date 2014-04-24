using System;
using System.IO;
using System.Text;
using System.Threading;
using GeoLib.Dal.Extensions;
using GeoLib.Dal.Helpers;
using GeoLib.Dal.Model;
using GeoLib.Dal.Model.Entities;
using GeoLib.Helpers;

namespace GeoLib.Parsing.GeoNames
{
    public class AdministrativeUnitsLevel2ParsingTask :
        ParsingTask
    {
        public AdministrativeUnitsLevel2ParsingTask(string path) : 
            base(new []{path})
        {
        }

        protected override void ExecuteInternal()
        {
            var stream = ResourceHelper.ReadFileContent(Path);

            using (var sr = new StreamReader(stream, Encoding.UTF8))
            {
                while (!sr.EndOfStream)
                {
                    using (var ctx = new GeoContext())
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
                                    var exists = ctx.AdministrativeUnits.FindAdministrativeUnit(ctry.Id, code, 2);
                                    if (exists != null)
                                        continue;

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
                                    ctx.SaveChanges();
                                    var aUnit = AdministrativeUnitDbSetExtensions.SaveAdministrativeUnit(ctry, code, ascii, name, 2, toponym != null ? (int?)toponym.Id : null, ctx);
                                    if (toponym != null)
                                    {
                                        toponym.Admin1 = possibleParent;
                                        toponym.Admin2 = aUnit;
                                    }
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