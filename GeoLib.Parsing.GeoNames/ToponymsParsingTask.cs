﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using GeoLib.Dal.Extensions;
using GeoLib.Dal.Model;
using GeoLib.Dal.Model.Entities;
using GeoLib.Helpers;

namespace GeoLib.Parsing.GeoNames
{
    public class ToponymsParsingTask :
        ParsingTask
    {
        protected long StartFrom { get; private set; }

        public ToponymsParsingTask(string path, long startFrom = 0) :
            base(new[] { path })
        {
            StartFrom = startFrom;
        }

        protected override void ExecuteInternal()
        {
            var stream = ResourceHelper.ReadFileContent(Path);

            using (var sr = new StreamReader(stream, Encoding.UTF8))
            {
                var parsed = 0;
                var lastSavedTFound = StartFrom == 0;
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

                            //Console.WriteLine(line);

                            var parts = line.Split(new[] { '\t' });
                            if (parts.Length < 19)
                                continue;

                            var sid = parts[0];
                            if (string.IsNullOrEmpty(sid))
                                continue;

                            var id = int.Parse(sid);
                            if (id < 0)
                                continue;

                            if (!lastSavedTFound)
                            {
                                lastSavedTFound = id == StartFrom;
                            }
                            if (!lastSavedTFound)
                                continue;

                            parsed++;
                            if (parsed % 1000 == 0)
                                Console.WriteLine(parsed);

                            var toponym = ctx.Toponyms.GetOrCreate(id);
                            var t = toponym.Entity;

                            if (toponym.IsNew)
                            {
                                t.DateCreated = DateTime.UtcNow;
                            }
                            ctx.Toponyms.PrepareToSave(toponym);
                            t.DateUpdated = DateTime.UtcNow;

                            t.Id = id;

                            var tname = parts[1];
                            var name = parts[2];
                            t.ToponymName = tname;
                            t.Name = name;

                            var nlang = ctx.Languages.FindLanguage("_");
                            if (nlang != null)
                            {
                                var ename = ctx.ToponymNames.GetOrCreate(t.Id, nlang.Id);
                                ename.Entity.ToponymId = t.Id;
                                ename.Entity.LanguageId = nlang.Id;
                                ename.Entity.Name = name;
                                ctx.ToponymNames.PrepareToSave(ename);
                            }

                            var slat = parts[4];
                            var slng = parts[5];
                            var sele = parts[15];

                            if (!string.IsNullOrEmpty(slat) && !string.IsNullOrEmpty(slng))
                            {
                                var lat = double.Parse(slat, CultureInfo.InvariantCulture);
                                var lng = double.Parse(slng, CultureInfo.InvariantCulture);
                                int? ele = null;
                                if (!string.IsNullOrEmpty(sele))
                                {
                                    ele = int.Parse(sele);
                                }
                                var location = LocationsDbSetExtensions.SaveToponymLocation(lat, lng, ele, ctx);
                                t.Location = location;
                            }

                            var fclass = parts[6];
                            if (!string.IsNullOrEmpty(fclass))
                            {
                                t.FeatureClassId = fclass;
                            }

                            var f = parts[7];
                            if (!string.IsNullOrEmpty(f))
                            {
                                t.FeatureId = f;
                            }

                            Country ctry = null;
                            var ccode = parts[8];
                            if (!string.IsNullOrEmpty(ccode))
                            {
                                ctry = ctx.Countries.GetByCode(ccode);
                                if (ctry != null)
                                {
                                    t.Country = ctry;
                                }
                            }
                            ctx.SaveChanges();
                            //var cc2 = parts[9];

                            #region AdmUnits

                            if (ctry != null)
                            {
                                var adm1Code = parts[10];
                                var adm2Code = parts[11];
                                var adm3Code = parts[12];
                                var adm4Code = parts[13];
                                var isAdm1 = !string.IsNullOrEmpty(adm1Code);
                                if (isAdm1)
                                {
                                    var existingUnit = ctx.AdministrativeUnits.FindAdministrativeUnit(ctry.Id, adm1Code, 1);
                                    var adm1ShouldBeSaved = string.IsNullOrEmpty(adm2Code) || existingUnit == null;
                                    var adm1Name = adm1ShouldBeSaved ? existingUnit != null ? name : adm1Code : null;
                                    var adm1TName = adm1ShouldBeSaved ? existingUnit != null ? tname : adm1Code : null;
                                    var adm1TId = adm1ShouldBeSaved ? (int?)id : null;
                                    var aUnit = AdministrativeUnitDbSetExtensions.SaveAdministrativeUnit(ctry, adm1Code, adm1Name, adm1TName, 1, adm1TId, ctx);
                                    t.Admin1 = aUnit;
                                }
                                var isAdm2 = !string.IsNullOrEmpty(adm2Code);
                                if (isAdm2)
                                {
                                    var existingUnit = ctx.AdministrativeUnits.FindAdministrativeUnit(ctry.Id, adm2Code, 2);
                                    var adm2ShouldBeSaved = string.IsNullOrEmpty(adm3Code) || existingUnit == null;
                                    var adm2Name = adm2ShouldBeSaved ? existingUnit != null ? name : adm2Code : null;
                                    var adm2TName = adm2ShouldBeSaved ? existingUnit != null ? tname : adm2Code : null;
                                    var adm2TId = adm2ShouldBeSaved ? (int?)id : null;
                                    var aUnit = AdministrativeUnitDbSetExtensions.SaveAdministrativeUnit(ctry, adm2Code, adm2Name, adm2TName, 2, adm2TId, ctx);
                                    t.Admin2 = aUnit;
                                }
                                var isAdm3 = !string.IsNullOrEmpty(adm3Code);
                                if (isAdm3)
                                {
                                    var existingUnit = ctx.AdministrativeUnits.FindAdministrativeUnit(ctry.Id, adm3Code, 3);
                                    var adm3ShouldBeSaved = string.IsNullOrEmpty(adm4Code) || existingUnit == null;
                                    var adm3Name = adm3ShouldBeSaved ? existingUnit != null ? name : adm3Code : null;
                                    var adm3TName = adm3ShouldBeSaved ? existingUnit != null ? tname : adm3Code : null;
                                    var adm3TId = adm3ShouldBeSaved ? (int?)id : null;
                                    var aUnit = AdministrativeUnitDbSetExtensions.SaveAdministrativeUnit(ctry, adm3Code, adm3Name, adm3TName, 3, adm3TId, ctx);
                                    t.Admin3 = aUnit;
                                }
                                ctx.SaveChanges();
                                var isAdm4 = !string.IsNullOrEmpty(adm4Code);
                                if (isAdm4)
                                {
                                    var adm4Name = name;
                                    var adm4TName = tname;
                                    var adm4TId = (int?)id;
                                    var aUnit = AdministrativeUnitDbSetExtensions.SaveAdministrativeUnit(ctry, adm3Code, adm4Name, adm4TName, 4, adm4TId, ctx);
                                    t.Admin4 = aUnit;
                                }
                                if (isAdm4)
                                {
                                    var possibleParent = t.Admin3;
                                    if (possibleParent != null)
                                    {
                                        t.ParentId = possibleParent.ToponymId;
                                    }
                                }
                                else
                                {
                                    if (isAdm3)
                                    {
                                        var possibleParent = t.Admin2;
                                        if (possibleParent != null)
                                        {
                                            t.ParentId = possibleParent.ToponymId;
                                        }
                                    }
                                    else
                                    {
                                        if (isAdm2)
                                        {
                                            var possibleParent = t.Admin1;
                                            if (possibleParent != null)
                                            {
                                                t.ParentId = possibleParent.ToponymId;
                                            }
                                        }
                                    }
                                }
                            }

                            #endregion

                            var spop = parts[14];
                            if (!string.IsNullOrEmpty(spop))
                            {
                                var pop = int.Parse(spop);
                                t.Population = pop;
                            }

                            //var sele = parts[15];

                            //var dem = parts[16];

                            var tzone = parts[17];
                            if (!string.IsNullOrEmpty(tzone))
                            {
                                t.TimeZone = ctx.TimeZones.FindTimeZone(tzone);
                            }

                            var mdate = parts[18];
                            if (!string.IsNullOrEmpty(mdate))
                            {
                                var pdate = mdate.Split(new[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
                                if (pdate.Length == 3)
                                {
                                    var year = int.Parse(pdate[0]);
                                    var month = int.Parse(pdate[1]);
                                    var day = int.Parse(pdate[2]);
                                    var date = new DateTime(year, month, day);
                                    t.DateSourceUpdated = date;
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