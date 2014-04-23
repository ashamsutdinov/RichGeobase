namespace GeoLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CIP : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CountryIPAddressBlocks",
                c => new
                    {
                        Start = c.Int(nullable: false),
                        End = c.Int(nullable: false),
                        CountryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Start, t.End })
                .ForeignKey("dbo.Countries", t => t.CountryId, cascadeDelete: true)
                .Index(t => t.CountryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CountryIPAddressBlocks", "CountryId", "dbo.Countries");
            DropIndex("dbo.CountryIPAddressBlocks", new[] { "CountryId" });
            DropTable("dbo.CountryIPAddressBlocks");
        }
    }
}
