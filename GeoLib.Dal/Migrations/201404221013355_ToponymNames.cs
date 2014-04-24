namespace GeoLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ToponymNames : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ToponymNames",
                c => new
                    {
                        ToponymId = c.Int(nullable: false),
                        LanguageId = c.String(nullable: false, maxLength: 128),
                        Name = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => new { t.ToponymId, t.LanguageId })
                .ForeignKey("dbo.Languages", t => t.LanguageId, cascadeDelete: true)
                .ForeignKey("dbo.Toponyms", t => t.ToponymId, cascadeDelete: true)
                .Index(t => t.ToponymId)
                .Index(t => t.LanguageId)
                .Index(t => t.Name);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ToponymNames", "ToponymId", "dbo.Toponyms");
            DropForeignKey("dbo.ToponymNames", "LanguageId", "dbo.Languages");
            DropIndex("dbo.ToponymNames", new[] { "Name" });
            DropIndex("dbo.ToponymNames", new[] { "LanguageId" });
            DropIndex("dbo.ToponymNames", new[] { "ToponymId" });
            DropTable("dbo.ToponymNames");
        }
    }
}
