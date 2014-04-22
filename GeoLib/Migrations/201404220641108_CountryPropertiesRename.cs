namespace GeoLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CountryPropertiesRename : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Countries", new[] { "CountryCode" });
            DropIndex("dbo.Countries", new[] { "CountryName" });
            AddColumn("dbo.Countries", "Code", c => c.String(maxLength: 16));
            AddColumn("dbo.Countries", "Name", c => c.String(maxLength: 128));
            CreateIndex("dbo.Countries", "Code");
            CreateIndex("dbo.Countries", "Name");
            DropColumn("dbo.Countries", "CountryCode");
            DropColumn("dbo.Countries", "CountryName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Countries", "CountryName", c => c.String(maxLength: 128));
            AddColumn("dbo.Countries", "CountryCode", c => c.String(maxLength: 16));
            DropIndex("dbo.Countries", new[] { "Name" });
            DropIndex("dbo.Countries", new[] { "Code" });
            DropColumn("dbo.Countries", "Name");
            DropColumn("dbo.Countries", "Code");
            CreateIndex("dbo.Countries", "CountryName");
            CreateIndex("dbo.Countries", "CountryCode");
        }
    }
}
