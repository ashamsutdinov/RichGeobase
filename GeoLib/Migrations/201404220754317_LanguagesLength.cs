namespace GeoLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LanguagesLength : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Languages", new[] { "IsoVariant1" });
            DropIndex("dbo.Languages", new[] { "IsoVariant2" });
            AlterColumn("dbo.Languages", "IsoVariant1", c => c.String(maxLength: 16));
            AlterColumn("dbo.Languages", "IsoVariant2", c => c.String(maxLength: 16));
            CreateIndex("dbo.Languages", "IsoVariant1");
            CreateIndex("dbo.Languages", "IsoVariant2");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Languages", new[] { "IsoVariant2" });
            DropIndex("dbo.Languages", new[] { "IsoVariant1" });
            AlterColumn("dbo.Languages", "IsoVariant2", c => c.String(maxLength: 8));
            AlterColumn("dbo.Languages", "IsoVariant1", c => c.String(maxLength: 8));
            CreateIndex("dbo.Languages", "IsoVariant2");
            CreateIndex("dbo.Languages", "IsoVariant1");
        }
    }
}
