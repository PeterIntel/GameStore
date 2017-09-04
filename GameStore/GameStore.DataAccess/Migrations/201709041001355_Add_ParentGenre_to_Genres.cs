namespace GameStore.DataAccess.MSSQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_ParentGenre_to_Genres : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.GenreEntities", new[] { "GenreEntity_Id" });
            DropColumn("dbo.GenreEntities", "ParentGenreId");
            RenameColumn(table: "dbo.GenreEntities", name: "GenreEntity_Id", newName: "ParentGenreId");
            AddColumn("dbo.PlatformTypeLocalEntities", "TypeName", c => c.String());
            AlterColumn("dbo.GenreEntities", "ParentGenreId", c => c.String(maxLength: 128));
            CreateIndex("dbo.GenreEntities", "ParentGenreId");
            DropColumn("dbo.PlatformTypeLocalEntities", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PlatformTypeLocalEntities", "Name", c => c.String());
            DropIndex("dbo.GenreEntities", new[] { "ParentGenreId" });
            AlterColumn("dbo.GenreEntities", "ParentGenreId", c => c.String());
            DropColumn("dbo.PlatformTypeLocalEntities", "TypeName");
            RenameColumn(table: "dbo.GenreEntities", name: "ParentGenreId", newName: "GenreEntity_Id");
            AddColumn("dbo.GenreEntities", "ParentGenreId", c => c.String());
            CreateIndex("dbo.GenreEntities", "GenreEntity_Id");
        }
    }
}
