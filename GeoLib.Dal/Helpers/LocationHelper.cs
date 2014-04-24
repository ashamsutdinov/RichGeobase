using System.Data.Entity;
using System.Linq;
using GeoLib.Dal.Model;
using GeoLib.Dal.Model.Entities;

namespace GeoLib.Dal.Helpers
{
    public static class LocationHelper
    {
        public static Location FindLocation(this DbSet<Location> dbset, double? latitude, double? longitude)
        {
            var found = dbset.FirstOrDefault(l => l.Latitude == latitude && l.Longitude == longitude);
            return found;
        }

        public static Location SaveToponymLocation(double latitude, double longitude, int? elevation, GeoContext context)
        {
            var ctx = context ?? new GeoContext();

            var location = ctx.Locations.FindLocation(latitude, longitude);
            if (location == null)
            {
                location = new Location();
                ctx.Locations.Add(location);
            }
            location.Latitude = latitude;
            location.Longitude = longitude;
            location.Elevation = elevation;

            if (context == null)
            {
                ctx.SaveChanges();
                ctx.Dispose();
            }

            return location;
        }

        public static Location SaveToponymLocation(NGeo.GeoNames.Toponym toponym, GeoContext context)
        {
            var ctx = context ?? new GeoContext();

            var location = ctx.Locations.FindLocation(toponym.Latitude, toponym.Longitude);
            if (location == null)
            {
                location = new Location();
                ctx.Locations.Add(location);
            }
            location.Latitude = toponym.Latitude;
            location.Longitude = toponym.Longitude;
            location.Elevation = toponym.Elevation;

            if (context == null)
            {
                ctx.SaveChanges();
                ctx.Dispose();
            }

            return location;
        }
    }
}