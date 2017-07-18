namespace GameStore.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Key_as_primaryKey_Games : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CommentEntities", "Game_Id", "dbo.GameEntities");
            DropForeignKey("dbo.GenreEntityGameEntities", "GameEntity_Id", "dbo.GameEntities");
            DropForeignKey("dbo.PlatformTypeEntityGameEntities", "GameEntity_Id", "dbo.GameEntities");
            DropIndex("dbo.CommentEntities", new[] { "Game_Id" });
            DropIndex("dbo.GameEntities", new[] { "Key" });
            DropIndex("dbo.GenreEntityGameEntities", new[] { "GameEntity_Id" });
            DropIndex("dbo.PlatformTypeEntityGameEntities", new[] { "GameEntity_Id" });
            DropColumn("dbo.CommentEntities", "GameKey");
            RenameColumn(table: "dbo.CommentEntities", name: "Game_Id", newName: "GameKey");
            RenameColumn(table: "dbo.GenreEntityGameEntities", name: "GameEntity_Id", newName: "GameEntity_Key");
            RenameColumn(table: "dbo.PlatformTypeEntityGameEntities", name: "GameEntity_Id", newName: "GameEntity_Key");
            DropPrimaryKey("dbo.GameEntities");
            DropPrimaryKey("dbo.GenreEntityGameEntities");
            DropPrimaryKey("dbo.PlatformTypeEntityGameEntities");
            AlterColumn("dbo.CommentEntities", "GameKey", c => c.String(maxLength: 450));
            AlterColumn("dbo.CommentEntities", "GameKey", c => c.String(maxLength: 450));
            AlterColumn("dbo.GameEntities", "Key", c => c.String(nullable: false, maxLength: 450));
            AlterColumn("dbo.GenreEntityGameEntities", "GameEntity_Key", c => c.String(nullable: false, maxLength: 450));
            AlterColumn("dbo.PlatformTypeEntityGameEntities", "GameEntity_Key", c => c.String(nullable: false, maxLength: 450));
            AddPrimaryKey("dbo.GameEntities", "Key");
            AddPrimaryKey("dbo.GenreEntityGameEntities", new[] { "GenreEntity_Id", "GameEntity_Key" });
            AddPrimaryKey("dbo.PlatformTypeEntityGameEntities", new[] { "PlatformTypeEntity_Id", "GameEntity_Key" });
            CreateIndex("dbo.CommentEntities", "GameKey");
            CreateIndex("dbo.GameEntities", "Key", unique: true);
            CreateIndex("dbo.GenreEntityGameEntities", "GameEntity_Key");
            CreateIndex("dbo.PlatformTypeEntityGameEntities", "GameEntity_Key");
            AddForeignKey("dbo.CommentEntities", "GameKey", "dbo.GameEntities", "Key");
            AddForeignKey("dbo.GenreEntityGameEntities", "GameEntity_Key", "dbo.GameEntities", "Key", cascadeDelete: true);
            AddForeignKey("dbo.PlatformTypeEntityGameEntities", "GameEntity_Key", "dbo.GameEntities", "Key", cascadeDelete: true);
            DropColumn("dbo.GameEntities", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.GameEntities", "Id", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.PlatformTypeEntityGameEntities", "GameEntity_Key", "dbo.GameEntities");
            DropForeignKey("dbo.GenreEntityGameEntities", "GameEntity_Key", "dbo.GameEntities");
            DropForeignKey("dbo.CommentEntities", "GameKey", "dbo.GameEntities");
            DropIndex("dbo.PlatformTypeEntityGameEntities", new[] { "GameEntity_Key" });
            DropIndex("dbo.GenreEntityGameEntities", new[] { "GameEntity_Key" });
            DropIndex("dbo.GameEntities", new[] { "Key" });
            DropIndex("dbo.CommentEntities", new[] { "GameKey" });
            DropPrimaryKey("dbo.PlatformTypeEntityGameEntities");
            DropPrimaryKey("dbo.GenreEntityGameEntities");
            DropPrimaryKey("dbo.GameEntities");
            AlterColumn("dbo.PlatformTypeEntityGameEntities", "GameEntity_Key", c => c.Int(nullable: false));
            AlterColumn("dbo.GenreEntityGameEntities", "GameEntity_Key", c => c.Int(nullable: false));
            AlterColumn("dbo.GameEntities", "Key", c => c.String(maxLength: 450));
            AlterColumn("dbo.CommentEntities", "GameKey", c => c.Int());
            AlterColumn("dbo.CommentEntities", "GameKey", c => c.String());
            AddPrimaryKey("dbo.PlatformTypeEntityGameEntities", new[] { "PlatformTypeEntity_Id", "GameEntity_Id" });
            AddPrimaryKey("dbo.GenreEntityGameEntities", new[] { "GenreEntity_Id", "GameEntity_Id" });
            AddPrimaryKey("dbo.GameEntities", "Id");
            RenameColumn(table: "dbo.PlatformTypeEntityGameEntities", name: "GameEntity_Key", newName: "GameEntity_Id");
            RenameColumn(table: "dbo.GenreEntityGameEntities", name: "GameEntity_Key", newName: "GameEntity_Id");
            RenameColumn(table: "dbo.CommentEntities", name: "GameKey", newName: "Game_Id");
            AddColumn("dbo.CommentEntities", "GameKey", c => c.String());
            CreateIndex("dbo.PlatformTypeEntityGameEntities", "GameEntity_Id");
            CreateIndex("dbo.GenreEntityGameEntities", "GameEntity_Id");
            CreateIndex("dbo.GameEntities", "Key", unique: true);
            CreateIndex("dbo.CommentEntities", "Game_Id");
            AddForeignKey("dbo.PlatformTypeEntityGameEntities", "GameEntity_Id", "dbo.GameEntities", "Id", cascadeDelete: true);
            AddForeignKey("dbo.GenreEntityGameEntities", "GameEntity_Id", "dbo.GameEntities", "Id", cascadeDelete: true);
            AddForeignKey("dbo.CommentEntities", "Game_Id", "dbo.GameEntities", "Id");
        }
    }
}
