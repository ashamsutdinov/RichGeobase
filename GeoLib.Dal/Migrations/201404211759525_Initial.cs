namespace GeoLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Continents",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        ToponymId = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Toponyms", t => t.ToponymId, cascadeDelete: true)
                .Index(t => t.ToponymId);
            
            CreateTable(
                "dbo.Toponyms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateUpdated = c.DateTime(),
                        Name = c.String(),
                        ToponymName = c.String(),
                        Population = c.Int(),
                        ContinentId = c.String(),
                        FeatureClassId = c.String(),
                        FeatureTypeId = c.String(),
                        Admin1Id = c.Int(),
                        Admin2Id = c.Int(),
                        Admin3Id = c.Int(),
                        Admin4Id = c.Int(),
                        TimeZoneId = c.String(),
                        ParentId = c.Int(),
                        LocationId = c.Int(),
                        BoundingBoxId = c.Int(),
                        PhoneCode = c.String(),
                        PostalCode = c.String(),
                        IATA = c.String(),
                        ICAO = c.String(),
                        FAAC = c.String(),
                        DateSourceUpdated = c.DateTime(),
                        IsCity = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Continents", "ToponymId", "dbo.Toponyms");
            DropIndex("dbo.Continents", new[] { "ToponymId" });
            DropTable("dbo.Toponyms");
            DropTable("dbo.Continents");
        }
    }
}
