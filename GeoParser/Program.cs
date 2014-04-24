using GeoLib.Dal.Helpers;
using GeoLib.Helpers;
using GeoLib.Dal.Model.Entities;
using GeoLib.Dal.Resources;
using GeoLib.Parsing;
using GeoLib.Parsing.GeoNames;

namespace GeoParser
{
    class Program
    {
        static void Main(string[] args)
        {
            var tasks = new ParsingTask[]
            {
                //TODO: local URLs with remote URLs
                //new LanguagesParsingTask(@"D:\Dropbox\Work\geobase\geonames\dump\iso-languagecodes.txt"),
                //new FeatureClassesParsingTask(@"D:\Dropbox\Work\geobase\geonames\dump\fclasses.txt"), 
                //new FeaturesParsingTask(@"D:\Dropbox\Work\geobase\geonames\dump\featureCodes_en.txt", "en"),
                //new FeaturesParsingTask(@"D:\Dropbox\Work\geobase\geonames\dump\featureCodes_bg.txt", "bg"),
                //new FeaturesParsingTask(@"D:\Dropbox\Work\geobase\geonames\dump\featureCodes_nb.txt", "nb"),
                //new FeaturesParsingTask(@"D:\Dropbox\Work\geobase\geonames\dump\featureCodes_nn.txt", "nn"),
                //new FeaturesParsingTask(@"D:\Dropbox\Work\geobase\geonames\dump\featureCodes_no.txt", "no"),
                //new FeaturesParsingTask(@"D:\Dropbox\Work\geobase\geonames\dump\featureCodes_ru.txt", "ru"),
                //new FeaturesParsingTask(@"D:\Dropbox\Work\geobase\geonames\dump\featureCodes_sv.txt", "sv"),
                //new TimeZonesParsingTask(@"D:\Dropbox\Work\geobase\geonames\dump\timeZones.txt"), 
                //new AdministrativeUnitsLevel1ParsingTask("http://download.geonames.org/export/dump/admin1CodesASCII.txt"),
                //new AdministrativeUnitsLevel2ParsingTask("http://download.geonames.org/export/dump/admin2Codes.txt"),
            };
            foreach (var parsingTask in tasks)
            {
                parsingTask.Execute();
            }
           
           //ContinentHelper.ParseContinents(GeoRes.continents);
            //CountryHelper.ParseCountries("http://download.geonames.org/export/dump/countryInfo.txt");
            
            //AdministrativeUnitHelper.ParseAdmin2Units("http://download.geonames.org/export/dump/admin2Codes.txt");
            ToponymHelper.ParseToponyms(@"D:\Dropbox\Work\geobase\geonames\dump\allCountries.txt");
            //ToponymHelper.ParseToponymNames("http://download.geonames.org/export/dump/alternateNames.zip");
            //CityHelper.ParseCities("http://download.geonames.org/export/dump/cities1000.zip", CitySize.Small);
            //CityHelper.ParseCities("http://download.geonames.org/export/dump/cities5000.zip", CitySize.Medium);
            //CityHelper.ParseCities("http://download.geonames.org/export/dump/cities15000.zip", CitySize.Large);
            //CountryHelper.ParseCountries("http://download.geonames.org/export/dump/countryInfo.txt", true, false);
            //CountryHelper.FillCapitalCities("http://download.geonames.org/export/dump/countryInfo.txt");
        }
    }
}
