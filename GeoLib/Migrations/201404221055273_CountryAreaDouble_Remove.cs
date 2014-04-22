namespace GeoLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CountryAreaDouble_Remove : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Countries", new[] { "Area" });
            DropColumn("dbo.Countries", "Area");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Countries", "Area", c => c.Single(nullable: false));
            CreateIndex("dbo.Countries", "Area");
        }
    }
}
