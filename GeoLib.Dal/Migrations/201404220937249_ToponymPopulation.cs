namespace GeoLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ToponymPopulation : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Toponyms", "Population", c => c.Long());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Toponyms", "Population", c => c.Int());
        }
    }
}
