namespace GeoLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ToponymDateIndices : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Toponyms", "DateCreated");
            CreateIndex("dbo.Toponyms", "DateUpdated");
            CreateIndex("dbo.Toponyms", "DateSourceUpdated");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Toponyms", new[] { "DateSourceUpdated" });
            DropIndex("dbo.Toponyms", new[] { "DateUpdated" });
            DropIndex("dbo.Toponyms", new[] { "DateCreated" });
        }
    }
}
