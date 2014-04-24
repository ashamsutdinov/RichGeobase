namespace GeoLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CountryNeighbors : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CountryCountries",
                c => new
                    {
                        Country_Id = c.Int(nullable: false),
                        Country_Id1 = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Country_Id, t.Country_Id1 })
                .ForeignKey("dbo.Countries", t => t.Country_Id)
                .ForeignKey("dbo.Countries", t => t.Country_Id1)
                .Index(t => t.Country_Id)
                .Index(t => t.Country_Id1);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CountryCountries", "Country_Id1", "dbo.Countries");
            DropForeignKey("dbo.CountryCountries", "Country_Id", "dbo.Countries");
            DropIndex("dbo.CountryCountries", new[] { "Country_Id1" });
            DropIndex("dbo.CountryCountries", new[] { "Country_Id" });
            DropTable("dbo.CountryCountries");
        }
    }
}
