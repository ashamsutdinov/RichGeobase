namespace GeoLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CountryLanguages : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LanguageCountries",
                c => new
                    {
                        Language_Id = c.String(nullable: false, maxLength: 128),
                        Country_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Language_Id, t.Country_Id })
                .ForeignKey("dbo.Languages", t => t.Language_Id, cascadeDelete: true)
                .ForeignKey("dbo.Countries", t => t.Country_Id, cascadeDelete: true)
                .Index(t => t.Language_Id)
                .Index(t => t.Country_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LanguageCountries", "Country_Id", "dbo.Countries");
            DropForeignKey("dbo.LanguageCountries", "Language_Id", "dbo.Languages");
            DropIndex("dbo.LanguageCountries", new[] { "Country_Id" });
            DropIndex("dbo.LanguageCountries", new[] { "Language_Id" });
            DropTable("dbo.LanguageCountries");
        }
    }
}
