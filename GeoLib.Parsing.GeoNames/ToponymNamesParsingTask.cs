using System;
using System.IO;
using System.Text;
using GeoLib.Dal.Extensions;
using GeoLib.Dal.Model;
using GeoLib.Helpers;

namespace GeoLib.Parsing.GeoNames
{
    public class ToponymNamesParsingTask :
        ParsingTask
    {
        public ToponymNamesParsingTask(string path) : 
            base(new []{path})
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

                            //Console.WriteLine(line);

                            var parts = line.Split(new[] { '\t' });
                            if (parts.Length < 4)
                                continue;

                            var sid = parts[1];
                            if (string.IsNullOrEmpty(sid))
                                continue;

                            var id = int.Parse(sid);

                            var t = ctx.Toponyms.GetById(id);
                            if (t == null)
                                continue;

                            var lang = parts[2];
                            var value = parts[3];

                            switch (lang)
                            {
                                case "post":
                                    t.PostalCode = value;
                                    break;
                                case "iata":
                                    t.IATA = value;
                                    break;
                                case "icao":
                                    t.ICAO = value;
                                    break;
                                case "faac":
                                    t.FAAC = value;
                                    break;
                                default:
                                    if (lang.Length < 4 || lang.Contains("-") || lang.Contains("/"))
                                    {
                                        var language = ctx.Languages.FindLanguage(lang);
                                        if (language == null)
                                            continue;

                                        var ename = ctx.ToponymNames.GetOrCreate(t.Id, language.Id);
                                        ename.Entity.ToponymId = t.Id;
                                        ename.Entity.LanguageId = language.Id;
                                        ename.Entity.Name = value;
                                        ctx.ToponymNames.PrepareToSave(ename);
                                    }
                                    break;
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