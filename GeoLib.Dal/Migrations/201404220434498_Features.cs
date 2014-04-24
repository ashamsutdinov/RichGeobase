namespace GeoLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Features : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Continents", "ToponymId", "dbo.Toponyms");
            DropPrimaryKey("dbo.Toponyms");
            CreateTable(
                "dbo.FeatureClasses",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FeatureNames",
                c => new
                    {
                        FeatureId = c.String(nullable: false, maxLength: 128),
                        LanguageId = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => new { t.FeatureId, t.LanguageId })
                .ForeignKey("dbo.Features", t => t.FeatureId, cascadeDelete: true)
                .ForeignKey("dbo.Languages", t => t.LanguageId, cascadeDelete: true)
                .Index(t => t.FeatureId)
                .Index(t => t.LanguageId);
            
            CreateTable(
                "dbo.Features",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FeatureClassId = c.String(maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FeatureClasses", t => t.FeatureClassId)
                .Index(t => t.FeatureClassId);
            
            CreateTable(
                "dbo.Languages",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Iso3 = c.String(),
                        Iso2 = c.String(),
                        Iso1 = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AlterColumn("dbo.Toponyms", "Id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Toponyms", "Id");
            AddForeignKey("dbo.Continents", "ToponymId", "dbo.Toponyms", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Continents", "ToponymId", "dbo.Toponyms");
            DropForeignKey("dbo.FeatureNames", "LanguageId", "dbo.Languages");
            DropForeignKey("dbo.FeatureNames", "FeatureId", "dbo.Features");
            DropForeignKey("dbo.Features", "FeatureClassId", "dbo.FeatureClasses");
            DropIndex("dbo.Features", new[] { "FeatureClassId" });
            DropIndex("dbo.FeatureNames", new[] { "LanguageId" });
            DropIndex("dbo.FeatureNames", new[] { "FeatureId" });
            DropPrimaryKey("dbo.Toponyms");
            AlterColumn("dbo.Toponyms", "Id", c => c.Int(nullable: false, identity: true));
            DropTable("dbo.Languages");
            DropTable("dbo.Features");
            DropTable("dbo.FeatureNames");
            DropTable("dbo.FeatureClasses");
            AddPrimaryKey("dbo.Toponyms", "Id");
            AddForeignKey("dbo.Continents", "ToponymId", "dbo.Toponyms", "Id", cascadeDelete: true);
        }
    }
}
