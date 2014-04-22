namespace GeoLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CountryCapitalCity_Removed : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Toponyms", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.Countries", "CapitalCityId", "dbo.Toponyms");
            DropIndex("dbo.Countries", new[] { "CapitalCityId" });
            DropIndex("dbo.Toponyms", new[] { "CountryId" });
            DropColumn("dbo.Countries", "CapitalCityId");
            DropColumn("dbo.Toponyms", "CountryId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Toponyms", "CountryId", c => c.Int());
            AddColumn("dbo.Countries", "CapitalCityId", c => c.Int(nullable: false));
            CreateIndex("dbo.Toponyms", "CountryId");
            CreateIndex("dbo.Countries", "CapitalCityId");
            AddForeignKey("dbo.Countries", "CapitalCityId", "dbo.Toponyms", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Toponyms", "CountryId", "dbo.Countries", "Id");
        }
    }
}
