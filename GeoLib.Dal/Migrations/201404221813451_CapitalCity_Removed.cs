namespace GeoLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CapitalCity_Removed : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Countries", "CapitalCity_Id", "dbo.Cities");
            DropIndex("dbo.Countries", new[] { "CapitalCity_Id" });
            DropIndex("dbo.Cities", new[] { "CapitalCityForCountryId" });
            DropColumn("dbo.Countries", "CapitalCity_Id");
            DropColumn("dbo.Cities", "CapitalCityForCountryId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Cities", "CapitalCityForCountryId", c => c.Int());
            AddColumn("dbo.Countries", "CapitalCity_Id", c => c.Int());
            CreateIndex("dbo.Cities", "CapitalCityForCountryId");
            CreateIndex("dbo.Countries", "CapitalCity_Id");
            AddForeignKey("dbo.Countries", "CapitalCity_Id", "dbo.Cities", "Id");
        }
    }
}
