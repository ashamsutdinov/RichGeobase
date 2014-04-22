using System.Data.Entity;
using GeoLib.Model.Entities;

namespace GeoLib.Model
{
    public class GeoContext : 
        DbContext
    {
        public GeoContext() :
            base("name=GeoDB")
        {
            
        }

        public DbSet<Continent> Continents { get; set; }

        public DbSet<Toponym> Toponyms { get; set; }

        public DbSet<Language> Languages { get; set; }

        public DbSet<FeatureClass> FeatureClasses { get; set; }

        public DbSet<Feature> Features { get; set; }

        public DbSet<FeatureName> FeatureNames { get; set; }

        public DbSet<TimeZone> TimeZones { get; set; }

        public DbSet<BoundingBox> BoundingBoxes { get; set; }

        public DbSet<Location> Locations { get; set; }
    }
}
