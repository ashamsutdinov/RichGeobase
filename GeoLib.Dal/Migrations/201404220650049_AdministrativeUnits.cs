namespace GeoLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdministrativeUnits : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AdministrativeUnits",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CountryId = c.Int(nullable: false),
                        Code = c.String(maxLength: 32),
                        Level = c.Int(nullable: false),
                        Name = c.String(maxLength: 256),
                        ToponymName = c.String(maxLength: 256),
                        ToponymId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.CountryId, cascadeDelete: true)
                .ForeignKey("dbo.Toponyms", t => t.ToponymId, cascadeDelete: false)
                .Index(t => t.CountryId)
                .Index(t => t.Code)
                .Index(t => t.Level)
                .Index(t => t.Name)
                .Index(t => t.ToponymName)
                .Index(t => t.ToponymId);
            
            CreateIndex("dbo.Toponyms", "Admin1Id");
            CreateIndex("dbo.Toponyms", "Admin2Id");
            CreateIndex("dbo.Toponyms", "Admin3Id");
            CreateIndex("dbo.Toponyms", "Admin4Id");
            AddForeignKey("dbo.Toponyms", "Admin1Id", "dbo.AdministrativeUnits", "Id");
            AddForeignKey("dbo.Toponyms", "Admin2Id", "dbo.AdministrativeUnits", "Id");
            AddForeignKey("dbo.Toponyms", "Admin3Id", "dbo.AdministrativeUnits", "Id");
            AddForeignKey("dbo.Toponyms", "Admin4Id", "dbo.AdministrativeUnits", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AdministrativeUnits", "ToponymId", "dbo.Toponyms");
            DropForeignKey("dbo.AdministrativeUnits", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.Toponyms", "Admin4Id", "dbo.AdministrativeUnits");
            DropForeignKey("dbo.Toponyms", "Admin3Id", "dbo.AdministrativeUnits");
            DropForeignKey("dbo.Toponyms", "Admin2Id", "dbo.AdministrativeUnits");
            DropForeignKey("dbo.Toponyms", "Admin1Id", "dbo.AdministrativeUnits");
            DropIndex("dbo.Toponyms", new[] { "Admin4Id" });
            DropIndex("dbo.Toponyms", new[] { "Admin3Id" });
            DropIndex("dbo.Toponyms", new[] { "Admin2Id" });
            DropIndex("dbo.Toponyms", new[] { "Admin1Id" });
            DropIndex("dbo.AdministrativeUnits", new[] { "ToponymId" });
            DropIndex("dbo.AdministrativeUnits", new[] { "ToponymName" });
            DropIndex("dbo.AdministrativeUnits", new[] { "Name" });
            DropIndex("dbo.AdministrativeUnits", new[] { "Level" });
            DropIndex("dbo.AdministrativeUnits", new[] { "Code" });
            DropIndex("dbo.AdministrativeUnits", new[] { "CountryId" });
            DropTable("dbo.AdministrativeUnits");
        }
    }
}
