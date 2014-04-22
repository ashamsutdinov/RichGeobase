namespace GeoLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Datetime2_Remove : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Countries", "DateCreated");
            DropColumn("dbo.Countries", "DateUpdated");
            DropColumn("dbo.Toponyms", "DateCreated");
            DropColumn("dbo.Toponyms", "DateUpdated");
            DropColumn("dbo.Toponyms", "DateSourceUpdated");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Toponyms", "DateSourceUpdated", c => c.DateTime());
            AddColumn("dbo.Toponyms", "DateUpdated", c => c.DateTime());
            AddColumn("dbo.Toponyms", "DateCreated", c => c.DateTime(nullable: false));
            AddColumn("dbo.Countries", "DateUpdated", c => c.DateTime());
            AddColumn("dbo.Countries", "DateCreated", c => c.DateTime(nullable: false));
        }
    }
}
