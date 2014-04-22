namespace GeoLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TimeZoneName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TimeZones", "Name", c => c.String(maxLength: 32));
            AlterColumn("dbo.TimeZones", "GmtOffset", c => c.Double(nullable: false));
            AlterColumn("dbo.TimeZones", "DstOffset", c => c.Double(nullable: false));
            AlterColumn("dbo.TimeZones", "RawOffset", c => c.Double(nullable: false));
            CreateIndex("dbo.TimeZones", "Name");
        }
        
        public override void Down()
        {
            DropIndex("dbo.TimeZones", new[] { "Name" });
            AlterColumn("dbo.TimeZones", "RawOffset", c => c.Int(nullable: false));
            AlterColumn("dbo.TimeZones", "DstOffset", c => c.Int(nullable: false));
            AlterColumn("dbo.TimeZones", "GmtOffset", c => c.Int(nullable: false));
            DropColumn("dbo.TimeZones", "Name");
        }
    }
}
