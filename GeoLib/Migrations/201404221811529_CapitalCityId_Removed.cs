namespace GeoLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CapitalCityId_Removed : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Countries", "CapitalCityId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Countries", "CapitalCityId", c => c.Int());
        }
    }
}
