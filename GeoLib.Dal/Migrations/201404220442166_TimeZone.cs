namespace GeoLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TimeZone : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TimeZones",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        GmtOffset = c.Int(nullable: false),
                        DstOffset = c.Int(nullable: false),
                        RawOffset = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Toponyms", "FeatureId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Toponyms", "FeatureClassId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Toponyms", "TimeZoneId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Toponyms", "FeatureClassId");
            CreateIndex("dbo.Toponyms", "FeatureId");
            CreateIndex("dbo.Toponyms", "TimeZoneId");
            CreateIndex("dbo.Toponyms", "ParentId");
            AddForeignKey("dbo.Toponyms", "FeatureId", "dbo.Features", "Id");
            AddForeignKey("dbo.Toponyms", "FeatureClassId", "dbo.FeatureClasses", "Id");
            AddForeignKey("dbo.Toponyms", "ParentId", "dbo.Toponyms", "Id");
            AddForeignKey("dbo.Toponyms", "TimeZoneId", "dbo.TimeZones", "Id");
            DropColumn("dbo.Toponyms", "FeatureTypeId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Toponyms", "FeatureTypeId", c => c.String());
            DropForeignKey("dbo.Toponyms", "TimeZoneId", "dbo.TimeZones");
            DropForeignKey("dbo.Toponyms", "ParentId", "dbo.Toponyms");
            DropForeignKey("dbo.Toponyms", "FeatureClassId", "dbo.FeatureClasses");
            DropForeignKey("dbo.Toponyms", "FeatureId", "dbo.Features");
            DropIndex("dbo.Toponyms", new[] { "ParentId" });
            DropIndex("dbo.Toponyms", new[] { "TimeZoneId" });
            DropIndex("dbo.Toponyms", new[] { "FeatureId" });
            DropIndex("dbo.Toponyms", new[] { "FeatureClassId" });
            AlterColumn("dbo.Toponyms", "TimeZoneId", c => c.String());
            AlterColumn("dbo.Toponyms", "FeatureClassId", c => c.String());
            DropColumn("dbo.Toponyms", "FeatureId");
            DropTable("dbo.TimeZones");
        }
    }
}
