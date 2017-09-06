namespace GameStore.DataAccess.MSSQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Delete_fields_from_localizedEntities : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.GenreEntities", new[] { "Name" });
            DropIndex("dbo.PlatformTypeEntities", new[] { "TypeName" });
            AlterColumn("dbo.GenreLocalEntities", "Name", c => c.String(maxLength: 450));
            AlterColumn("dbo.PlatformTypeLocalEntities", "TypeName", c => c.String(maxLength: 450));
            AlterColumn("dbo.PublisherLocalEntities", "Description", c => c.String(storeType: "ntext"));
            CreateIndex("dbo.GenreLocalEntities", "Name", unique: true);
            CreateIndex("dbo.PlatformTypeLocalEntities", "TypeName", unique: true);
            DropColumn("dbo.GameEntities", "Description");
            DropColumn("dbo.GenreEntities", "Name");
            DropColumn("dbo.PlatformTypeEntities", "TypeName");
            DropColumn("dbo.PublisherEntities", "Description");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PublisherEntities", "Description", c => c.String(storeType: "ntext"));
            AddColumn("dbo.PlatformTypeEntities", "TypeName", c => c.String(maxLength: 450));
            AddColumn("dbo.GenreEntities", "Name", c => c.String(maxLength: 450));
            AddColumn("dbo.GameEntities", "Description", c => c.String());
            DropIndex("dbo.PlatformTypeLocalEntities", new[] { "TypeName" });
            DropIndex("dbo.GenreLocalEntities", new[] { "Name" });
            AlterColumn("dbo.PublisherLocalEntities", "Description", c => c.String());
            AlterColumn("dbo.PlatformTypeLocalEntities", "TypeName", c => c.String());
            AlterColumn("dbo.GenreLocalEntities", "Name", c => c.String());
            CreateIndex("dbo.PlatformTypeEntities", "TypeName", unique: true);
            CreateIndex("dbo.GenreEntities", "Name", unique: true);
        }
    }
}
