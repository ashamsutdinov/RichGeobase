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

        public DbSet<Currency> Currencies { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<AdministrativeUnit> AdministrativeUnits { get; set; }

        public DbSet<ToponymName> ToponymNames { get; set; }

        public DbSet<City> Cities { get; set; }

        public DbSet<CountryIPAddressBlock> CountryIPAddressBlocks { get; set; } 

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Country>().HasOptional(c => c.CapitalCity).WithOptionalDependent(ct => ct.CapitalCityForCountry);
            modelBuilder.Entity<Toponym>().HasOptional(t => t.Continent).WithMany(t => t.Toponyms);
            modelBuilder.Entity<Toponym>().HasOptional(t => t.Country).WithMany(t => t.Toponyms);
            modelBuilder.Entity<Country>().HasMany(t => t.Neighbors).WithMany(t => t.NeighborTo);
            modelBuilder.Entity<Country>().HasMany(t => t.Cities).WithRequired(c => c.Country);
        }
    }
}
