namespace GeoLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CapitalCity_Restored : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Countries", "CapitalCity_Id", c => c.Int());
            AddColumn("dbo.Cities", "CapitalCityForCountryId", c => c.Int());
            CreateIndex("dbo.Countries", "CapitalCity_Id");
            CreateIndex("dbo.Cities", "CapitalCityForCountryId");
            AddForeignKey("dbo.Countries", "CapitalCity_Id", "dbo.Cities", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Countries", "CapitalCity_Id", "dbo.Cities");
            DropIndex("dbo.Cities", new[] { "CapitalCityForCountryId" });
            DropIndex("dbo.Countries", new[] { "CapitalCity_Id" });
            DropColumn("dbo.Cities", "CapitalCityForCountryId");
            DropColumn("dbo.Countries", "CapitalCity_Id");
        }
    }
}
