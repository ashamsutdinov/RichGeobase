namespace GeoLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ToponymCountry : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Toponyms", "CountryId", c => c.Int());
            CreateIndex("dbo.Toponyms", "CountryId");
            AddForeignKey("dbo.Toponyms", "CountryId", "dbo.Countries", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Toponyms", "CountryId", "dbo.Countries");
            DropIndex("dbo.Toponyms", new[] { "CountryId" });
            DropColumn("dbo.Toponyms", "CountryId");
        }
    }
}
