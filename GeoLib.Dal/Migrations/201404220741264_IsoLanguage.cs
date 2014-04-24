namespace GeoLib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsoLanguage : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Languages", new[] { "Iso3" });
            DropIndex("dbo.Languages", new[] { "Iso2" });
            DropIndex("dbo.Languages", new[] { "Iso1" });
            AddColumn("dbo.Languages", "IsoVariant1", c => c.String(maxLength: 8));
            AddColumn("dbo.Languages", "IsoVariant2", c => c.String(maxLength: 8));
            CreateIndex("dbo.Languages", "IsoVariant1");
            CreateIndex("dbo.Languages", "IsoVariant2");
            DropColumn("dbo.Languages", "Iso3");
            DropColumn("dbo.Languages", "Iso2");
            DropColumn("dbo.Languages", "Iso1");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Languages", "Iso1", c => c.String(maxLength: 8));
            AddColumn("dbo.Languages", "Iso2", c => c.String(maxLength: 8));
            AddColumn("dbo.Languages", "Iso3", c => c.String(maxLength: 8));
            DropIndex("dbo.Languages", new[] { "IsoVariant2" });
            DropIndex("dbo.Languages", new[] { "IsoVariant1" });
            DropColumn("dbo.Languages", "IsoVariant2");
            DropColumn("dbo.Languages", "IsoVariant1");
            CreateIndex("dbo.Languages", "Iso1");
            CreateIndex("dbo.Languages", "Iso2");
            CreateIndex("dbo.Languages", "Iso3");
        }
    }
}
