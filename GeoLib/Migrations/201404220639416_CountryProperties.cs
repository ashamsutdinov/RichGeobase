namespace GeoLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CountryProperties : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Countries", "DateCreated", c => c.DateTime(nullable: false));
            AddColumn("dbo.Countries", "DateUpdated", c => c.DateTime());
            AddColumn("dbo.Countries", "ContinentId", c => c.String(maxLength: 128));
            AddColumn("dbo.Countries", "CountryCode", c => c.String(maxLength: 16));
            AddColumn("dbo.Countries", "CountryName", c => c.String(maxLength: 128));
            AddColumn("dbo.Countries", "IsoNumeric", c => c.Int(nullable: false));
            AddColumn("dbo.Countries", "IsoAlpha", c => c.String(maxLength: 16));
            AddColumn("dbo.Countries", "Fips", c => c.String(maxLength: 16));
            AddColumn("dbo.Countries", "CapitalCityId", c => c.Int(nullable: false));
            AddColumn("dbo.Countries", "Population", c => c.Int(nullable: false));
            AddColumn("dbo.Countries", "Area", c => c.Single(nullable: false));
            AddColumn("dbo.Countries", "CurrencyId", c => c.String(maxLength: 128));
            AddColumn("dbo.Countries", "Domain", c => c.String(maxLength: 16));
            AddColumn("dbo.Countries", "PhoneCode", c => c.String());
            AddColumn("dbo.Countries", "PostalCodeFormat", c => c.String());
            AddColumn("dbo.Countries", "PostalCodeRegex", c => c.String());
            CreateIndex("dbo.Countries", "ContinentId");
            CreateIndex("dbo.Countries", "CountryCode");
            CreateIndex("dbo.Countries", "CountryName");
            CreateIndex("dbo.Countries", "IsoNumeric");
            CreateIndex("dbo.Countries", "IsoAlpha");
            CreateIndex("dbo.Countries", "Fips");
            CreateIndex("dbo.Countries", "CapitalCityId");
            CreateIndex("dbo.Countries", "Population");
            CreateIndex("dbo.Countries", "Area");
            CreateIndex("dbo.Countries", "CurrencyId");
            CreateIndex("dbo.Countries", "Domain");
            AddForeignKey("dbo.Countries", "CapitalCityId", "dbo.Toponyms", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Countries", "ContinentId", "dbo.Continents", "Id");
            AddForeignKey("dbo.Countries", "CurrencyId", "dbo.Currencies", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Countries", "CurrencyId", "dbo.Currencies");
            DropForeignKey("dbo.Countries", "ContinentId", "dbo.Continents");
            DropForeignKey("dbo.Countries", "CapitalCityId", "dbo.Toponyms");
            DropIndex("dbo.Countries", new[] { "Domain" });
            DropIndex("dbo.Countries", new[] { "CurrencyId" });
            DropIndex("dbo.Countries", new[] { "Area" });
            DropIndex("dbo.Countries", new[] { "Population" });
            DropIndex("dbo.Countries", new[] { "CapitalCityId" });
            DropIndex("dbo.Countries", new[] { "Fips" });
            DropIndex("dbo.Countries", new[] { "IsoAlpha" });
            DropIndex("dbo.Countries", new[] { "IsoNumeric" });
            DropIndex("dbo.Countries", new[] { "CountryName" });
            DropIndex("dbo.Countries", new[] { "CountryCode" });
            DropIndex("dbo.Countries", new[] { "ContinentId" });
            DropColumn("dbo.Countries", "PostalCodeRegex");
            DropColumn("dbo.Countries", "PostalCodeFormat");
            DropColumn("dbo.Countries", "PhoneCode");
            DropColumn("dbo.Countries", "Domain");
            DropColumn("dbo.Countries", "CurrencyId");
            DropColumn("dbo.Countries", "Area");
            DropColumn("dbo.Countries", "Population");
            DropColumn("dbo.Countries", "CapitalCityId");
            DropColumn("dbo.Countries", "Fips");
            DropColumn("dbo.Countries", "IsoAlpha");
            DropColumn("dbo.Countries", "IsoNumeric");
            DropColumn("dbo.Countries", "CountryName");
            DropColumn("dbo.Countries", "CountryCode");
            DropColumn("dbo.Countries", "ContinentId");
            DropColumn("dbo.Countries", "DateUpdated");
            DropColumn("dbo.Countries", "DateCreated");
        }
    }
}
