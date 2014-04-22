namespace GeoLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Datetime2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Toponyms", "DateCreated", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Toponyms", "DateUpdated", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Toponyms", "DateSourceUpdated", c => c.DateTime(precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Toponyms", "DateSourceUpdated");
            DropColumn("dbo.Toponyms", "DateUpdated");
            DropColumn("dbo.Toponyms", "DateCreated");
        }
    }
}
