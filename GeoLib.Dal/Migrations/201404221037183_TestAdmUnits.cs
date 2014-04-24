namespace GeoLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TestAdmUnits : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AdministrativeUnits", "ToponymId", "dbo.Toponyms");
            DropIndex("dbo.AdministrativeUnits", new[] { "CountryId" });
            DropIndex("dbo.AdministrativeUnits", new[] { "ToponymId" });
            AlterColumn("dbo.AdministrativeUnits", "ToponymId", c => c.Int());
            CreateIndex("dbo.AdministrativeUnits", "CountryId");
            CreateIndex("dbo.AdministrativeUnits", "ToponymId");
            AddForeignKey("dbo.AdministrativeUnits", "ToponymId", "dbo.Toponyms", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AdministrativeUnits", "ToponymId", "dbo.Toponyms");
            DropIndex("dbo.AdministrativeUnits", new[] { "ToponymId" });
            DropIndex("dbo.AdministrativeUnits", new[] { "CountryId" });
            AlterColumn("dbo.AdministrativeUnits", "ToponymId", c => c.Int(nullable: false));
            CreateIndex("dbo.AdministrativeUnits", "ToponymId");
            CreateIndex("dbo.AdministrativeUnits", "CountryId");
            AddForeignKey("dbo.AdministrativeUnits", "ToponymId", "dbo.Toponyms", "Id", cascadeDelete: true);
        }
    }
}
