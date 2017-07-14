namespace GameStore.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IENUMARABLE_TO_LIST : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GenreEntityGameEntities",
                c => new
                    {
                        GenreEntity_Id = c.Int(nullable: false),
                        GameEntity_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.GenreEntity_Id, t.GameEntity_Id })
                .ForeignKey("dbo.GenreEntities", t => t.GenreEntity_Id, cascadeDelete: true)
                .ForeignKey("dbo.GameEntities", t => t.GameEntity_Id, cascadeDelete: true)
                .Index(t => t.GenreEntity_Id)
                .Index(t => t.GameEntity_Id);
            
            CreateTable(
                "dbo.PlatformTypeEntityGameEntities",
                c => new
                    {
                        PlatformTypeEntity_Id = c.Int(nullable: false),
                        GameEntity_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PlatformTypeEntity_Id, t.GameEntity_Id })
                .ForeignKey("dbo.PlatformTypeEntities", t => t.PlatformTypeEntity_Id, cascadeDelete: true)
                .ForeignKey("dbo.GameEntities", t => t.GameEntity_Id, cascadeDelete: true)
                .Index(t => t.PlatformTypeEntity_Id)
                .Index(t => t.GameEntity_Id);
            
            AddColumn("dbo.CommentEntities", "CommentEntity_Id", c => c.Int());
            AddColumn("dbo.GenreEntities", "GenreEntity_Id", c => c.Int());
            CreateIndex("dbo.CommentEntities", "CommentEntity_Id");
            CreateIndex("dbo.GenreEntities", "GenreEntity_Id");
            AddForeignKey("dbo.CommentEntities", "CommentEntity_Id", "dbo.CommentEntities", "Id");
            AddForeignKey("dbo.GenreEntities", "GenreEntity_Id", "dbo.GenreEntities", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PlatformTypeEntityGameEntities", "GameEntity_Id", "dbo.GameEntities");
            DropForeignKey("dbo.PlatformTypeEntityGameEntities", "PlatformTypeEntity_Id", "dbo.PlatformTypeEntities");
            DropForeignKey("dbo.GenreEntities", "GenreEntity_Id", "dbo.GenreEntities");
            DropForeignKey("dbo.GenreEntityGameEntities", "GameEntity_Id", "dbo.GameEntities");
            DropForeignKey("dbo.GenreEntityGameEntities", "GenreEntity_Id", "dbo.GenreEntities");
            DropForeignKey("dbo.CommentEntities", "CommentEntity_Id", "dbo.CommentEntities");
            DropIndex("dbo.PlatformTypeEntityGameEntities", new[] { "GameEntity_Id" });
            DropIndex("dbo.PlatformTypeEntityGameEntities", new[] { "PlatformTypeEntity_Id" });
            DropIndex("dbo.GenreEntityGameEntities", new[] { "GameEntity_Id" });
            DropIndex("dbo.GenreEntityGameEntities", new[] { "GenreEntity_Id" });
            DropIndex("dbo.GenreEntities", new[] { "GenreEntity_Id" });
            DropIndex("dbo.CommentEntities", new[] { "CommentEntity_Id" });
            DropColumn("dbo.GenreEntities", "GenreEntity_Id");
            DropColumn("dbo.CommentEntities", "CommentEntity_Id");
            DropTable("dbo.PlatformTypeEntityGameEntities");
            DropTable("dbo.GenreEntityGameEntities");
        }
    }
}
