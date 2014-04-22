namespace GeoLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CountryAreaDouble : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Countries", "Area", c => c.Double(nullable: false));
            CreateIndex("dbo.Countries", "Area");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Countries", new[] { "Area" });
            DropColumn("dbo.Countries", "Area");
        }
    }
}
