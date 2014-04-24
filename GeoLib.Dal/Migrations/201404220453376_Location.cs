namespace GeoLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Location : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BoundingBoxes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        West = c.Single(),
                        North = c.Single(),
                        East = c.Single(),
                        South = c.Single(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.West)
                .Index(t => t.North)
                .Index(t => t.East)
                .Index(t => t.South);
            
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Latitude = c.Single(),
                        Longitude = c.Single(),
                        Elevation = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Latitude)
                .Index(t => t.Longitude);
            
            AlterColumn("dbo.Languages", "Iso3", c => c.String(maxLength: 8));
            AlterColumn("dbo.Languages", "Iso2", c => c.String(maxLength: 8));
            AlterColumn("dbo.Languages", "Iso1", c => c.String(maxLength: 8));
            CreateIndex("dbo.Toponyms", "LocationId");
            CreateIndex("dbo.Toponyms", "BoundingBoxId");
            CreateIndex("dbo.Languages", "Iso3");
            CreateIndex("dbo.Languages", "Iso2");
            CreateIndex("dbo.Languages", "Iso1");
            AddForeignKey("dbo.Toponyms", "BoundingBoxId", "dbo.BoundingBoxes", "Id");
            AddForeignKey("dbo.Toponyms", "LocationId", "dbo.Locations", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Toponyms", "LocationId", "dbo.Locations");
            DropForeignKey("dbo.Toponyms", "BoundingBoxId", "dbo.BoundingBoxes");
            DropIndex("dbo.Languages", new[] { "Iso1" });
            DropIndex("dbo.Languages", new[] { "Iso2" });
            DropIndex("dbo.Languages", new[] { "Iso3" });
            DropIndex("dbo.Locations", new[] { "Longitude" });
            DropIndex("dbo.Locations", new[] { "Latitude" });
            DropIndex("dbo.Toponyms", new[] { "BoundingBoxId" });
            DropIndex("dbo.Toponyms", new[] { "LocationId" });
            DropIndex("dbo.BoundingBoxes", new[] { "South" });
            DropIndex("dbo.BoundingBoxes", new[] { "East" });
            DropIndex("dbo.BoundingBoxes", new[] { "North" });
            DropIndex("dbo.BoundingBoxes", new[] { "West" });
            AlterColumn("dbo.Languages", "Iso1", c => c.String());
            AlterColumn("dbo.Languages", "Iso2", c => c.String());
            AlterColumn("dbo.Languages", "Iso3", c => c.String());
            DropTable("dbo.Locations");
            DropTable("dbo.BoundingBoxes");
        }
    }
}
