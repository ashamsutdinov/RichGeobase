namespace GeoLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GeoRelations : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Toponyms", new[] { "ContinentId" });
            AddColumn("dbo.Countries", "CapitalCityId", c => c.Int());
            AddColumn("dbo.Toponyms", "CountryId", c => c.Int());
            AlterColumn("dbo.Toponyms", "ContinentId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Countries", "CapitalCityId");
            CreateIndex("dbo.Toponyms", "ContinentId");
            CreateIndex("dbo.Toponyms", "CountryId");
            AddForeignKey("dbo.Toponyms", "ContinentId", "dbo.Continents", "Id");
            AddForeignKey("dbo.Toponyms", "CountryId", "dbo.Countries", "Id");
            AddForeignKey("dbo.Countries", "CapitalCityId", "dbo.Toponyms", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Countries", "CapitalCityId", "dbo.Toponyms");
            DropForeignKey("dbo.Toponyms", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.Toponyms", "ContinentId", "dbo.Continents");
            DropIndex("dbo.Toponyms", new[] { "CountryId" });
            DropIndex("dbo.Toponyms", new[] { "ContinentId" });
            DropIndex("dbo.Countries", new[] { "CapitalCityId" });
            AlterColumn("dbo.Toponyms", "ContinentId", c => c.String(maxLength: 8));
            DropColumn("dbo.Toponyms", "CountryId");
            DropColumn("dbo.Countries", "CapitalCityId");
            CreateIndex("dbo.Toponyms", "ContinentId");
        }
    }
}
