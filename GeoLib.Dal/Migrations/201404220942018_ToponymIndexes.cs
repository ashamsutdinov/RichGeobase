namespace GeoLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ToponymIndexes : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Toponyms", "Name", c => c.String(maxLength: 256));
            AlterColumn("dbo.Toponyms", "ToponymName", c => c.String(maxLength: 256));
            AlterColumn("dbo.Toponyms", "ContinentId", c => c.String(maxLength: 8));
            CreateIndex("dbo.Toponyms", "Name");
            CreateIndex("dbo.Toponyms", "ToponymName");
            CreateIndex("dbo.Toponyms", "ContinentId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Toponyms", new[] { "ContinentId" });
            DropIndex("dbo.Toponyms", new[] { "ToponymName" });
            DropIndex("dbo.Toponyms", new[] { "Name" });
            AlterColumn("dbo.Toponyms", "ContinentId", c => c.String());
            AlterColumn("dbo.Toponyms", "ToponymName", c => c.String());
            AlterColumn("dbo.Toponyms", "Name", c => c.String());
        }
    }
}
