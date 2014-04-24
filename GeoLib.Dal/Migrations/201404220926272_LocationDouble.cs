namespace GeoLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LocationDouble : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Locations", new[] { "Latitude" });
            DropIndex("dbo.Locations", new[] { "Longitude" });
            AlterColumn("dbo.Locations", "Latitude", c => c.Double());
            AlterColumn("dbo.Locations", "Longitude", c => c.Double());
            CreateIndex("dbo.Locations", "Latitude");
            CreateIndex("dbo.Locations", "Longitude");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Locations", new[] { "Longitude" });
            DropIndex("dbo.Locations", new[] { "Latitude" });
            AlterColumn("dbo.Locations", "Longitude", c => c.Single());
            AlterColumn("dbo.Locations", "Latitude", c => c.Single());
            CreateIndex("dbo.Locations", "Longitude");
            CreateIndex("dbo.Locations", "Latitude");
        }
    }
}
