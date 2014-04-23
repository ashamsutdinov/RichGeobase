namespace GeoLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ToponymTags : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ToponymTags",
                c => new
                    {
                        ToponymId = c.Int(nullable: false),
                        Key = c.String(nullable: false, maxLength: 128),
                        Value = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => new { t.ToponymId, t.Key })
                .ForeignKey("dbo.Toponyms", t => t.ToponymId, cascadeDelete: true)
                .Index(t => t.ToponymId)
                .Index(t => t.Value);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ToponymTags", "ToponymId", "dbo.Toponyms");
            DropIndex("dbo.ToponymTags", new[] { "Value" });
            DropIndex("dbo.ToponymTags", new[] { "ToponymId" });
            DropTable("dbo.ToponymTags");
        }
    }
}
