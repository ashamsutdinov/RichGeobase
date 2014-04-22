namespace GeoLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Cities : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        CountryId = c.Int(nullable: false),
                        Size = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.CountryId, cascadeDelete: true)
                .ForeignKey("dbo.Toponyms", t => t.Id)
                .Index(t => t.Id)
                .Index(t => t.CountryId)
                .Index(t => t.Size);
            
            DropColumn("dbo.Toponyms", "IsCity");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Toponyms", "IsCity", c => c.Boolean(nullable: false));
            DropForeignKey("dbo.Cities", "Id", "dbo.Toponyms");
            DropForeignKey("dbo.Cities", "CountryId", "dbo.Countries");
            DropIndex("dbo.Cities", new[] { "Size" });
            DropIndex("dbo.Cities", new[] { "CountryId" });
            DropIndex("dbo.Cities", new[] { "Id" });
            DropTable("dbo.Cities");
        }
    }
}
