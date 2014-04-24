using GeoLib.Dal.Helpers;
using GeoLib.Helpers;
using GeoLib.Dal.Model.Entities;
using GeoLib.Dal.Resources;
using GeoLib.Parsing;

namespace GeoParser
{
    class Program
    {
        static void Main(string[] args)
        {
            var tasks = new ParsingTask[]
            {

            };
            foreach (var parsingTask in tasks)
            {
                parsingTask.Execute();
            }
            //LanguageHelper.ParseLanguages(GeoRes.iso_languagecodes);
            //FeatureClassHelper.ParseFearureClasses(GeoRes.fclasses);
            //FeatureHelper.ParseFeature(GeoRes.featureCodes_en, "en");
            //FeatureHelper.ParseFeature(GeoRes.featureCodes_bg, "bg");
            //FeatureHelper.ParseFeature(GeoRes.featureCodes_nb, "nb");
            //FeatureHelper.ParseFeature(GeoRes.featureCodes_nn, "nn");
            //FeatureHelper.ParseFeature(GeoRes.featureCodes_no, "no");
            //FeatureHelper.ParseFeature(GeoRes.featureCodes_ru, "ru");
            //FeatureHelper.ParseFeature(GeoRes.featureCodes_sv, "sv");
            //TimeZoneHelper.ParseFeature(GeoRes.timeZones);
            //ContinentHelper.ParseContinents(GeoRes.continents);
            //CountryHelper.ParseCountries("http://download.geonames.org/export/dump/countryInfo.txt");
            //AdministrativeUnitHelper.ParseAdmin1Units("http://download.geonames.org/export/dump/admin1CodesASCII.txt");
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
