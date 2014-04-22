namespace GeoLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ContinentToponymNullable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Continents", "ToponymId", "dbo.Toponyms");
            DropIndex("dbo.Continents", new[] { "ToponymId" });
            AlterColumn("dbo.Continents", "ToponymId", c => c.Int());
            CreateIndex("dbo.Continents", "ToponymId");
            AddForeignKey("dbo.Continents", "ToponymId", "dbo.Toponyms", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Continents", "ToponymId", "dbo.Toponyms");
            DropIndex("dbo.Continents", new[] { "ToponymId" });
            AlterColumn("dbo.Continents", "ToponymId", c => c.Int(nullable: false));
            CreateIndex("dbo.Continents", "ToponymId");
            AddForeignKey("dbo.Continents", "ToponymId", "dbo.Toponyms", "Id", cascadeDelete: true);
        }
    }
}
