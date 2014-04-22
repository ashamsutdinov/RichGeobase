using GeoLib.Helpers;

namespace GeoParser
{
    class Program
    {
        static void Main(string[] args)
        {
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
            AdministrativeUnitHelper.ParseAdmin1Units("http://download.geonames.org/export/dump/admin1CodesASCII.txt");
            AdministrativeUnitHelper.ParseAdmin2Units("http://download.geonames.org/export/dump/admin2Codes.txt");
            ToponymHelper.ParseToponyms("http://download.geonames.org/export/dump/allCountries.zip");
            ToponymHelper.ParseToponymNames("http://download.geonames.org/export/dump/alternateNames.zip");
        }
    }
}
