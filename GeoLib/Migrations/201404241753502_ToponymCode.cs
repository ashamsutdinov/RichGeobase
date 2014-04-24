namespace GeoLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ToponymCode : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.AdministrativeUnits", new[] { "Code" });
            AlterColumn("dbo.AdministrativeUnits", "Code", c => c.String(maxLength: 128));
            CreateIndex("dbo.AdministrativeUnits", "Code");
        }
        
        public override void Down()
        {
            DropIndex("dbo.AdministrativeUnits", new[] { "Code" });
            AlterColumn("dbo.AdministrativeUnits", "Code", c => c.String(maxLength: 32));
            CreateIndex("dbo.AdministrativeUnits", "Code");
        }
    }
}
