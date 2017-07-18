namespace GameStore.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CommentEntities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Body = c.String(),
                        ParentCommentId = c.Int(),
                        GameKey = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        CommentEntity_Id = c.Int(),
                        Game_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CommentEntities", t => t.CommentEntity_Id)
                .ForeignKey("dbo.GameEntities", t => t.Game_Id)
                .Index(t => t.CommentEntity_Id)
                .Index(t => t.Game_Id);
            
            CreateTable(
                "dbo.GameEntities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Key = c.String(maxLength: 450),
                        Description = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Key, unique: true);
            
            CreateTable(
                "dbo.GenreEntities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 450),
                        ParentGenreId = c.Int(),
                        IsDeleted = c.Boolean(nullable: false),
                        GenreEntity_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GenreEntities", t => t.GenreEntity_Id)
                .Index(t => t.Name, unique: true)
                .Index(t => t.GenreEntity_Id);
            
            CreateTable(
                "dbo.PlatformTypeEntities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TypeName = c.String(maxLength: 450),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.TypeName, unique: true);
            
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PlatformTypeEntityGameEntities", "GameEntity_Id", "dbo.GameEntities");
            DropForeignKey("dbo.PlatformTypeEntityGameEntities", "PlatformTypeEntity_Id", "dbo.PlatformTypeEntities");
            DropForeignKey("dbo.GenreEntities", "GenreEntity_Id", "dbo.GenreEntities");
            DropForeignKey("dbo.GenreEntityGameEntities", "GameEntity_Id", "dbo.GameEntities");
            DropForeignKey("dbo.GenreEntityGameEntities", "GenreEntity_Id", "dbo.GenreEntities");
            DropForeignKey("dbo.CommentEntities", "Game_Id", "dbo.GameEntities");
            DropForeignKey("dbo.CommentEntities", "CommentEntity_Id", "dbo.CommentEntities");
            DropIndex("dbo.PlatformTypeEntityGameEntities", new[] { "GameEntity_Id" });
            DropIndex("dbo.PlatformTypeEntityGameEntities", new[] { "PlatformTypeEntity_Id" });
            DropIndex("dbo.GenreEntityGameEntities", new[] { "GameEntity_Id" });
            DropIndex("dbo.GenreEntityGameEntities", new[] { "GenreEntity_Id" });
            DropIndex("dbo.PlatformTypeEntities", new[] { "TypeName" });
            DropIndex("dbo.GenreEntities", new[] { "GenreEntity_Id" });
            DropIndex("dbo.GenreEntities", new[] { "Name" });
            DropIndex("dbo.GameEntities", new[] { "Key" });
            DropIndex("dbo.CommentEntities", new[] { "Game_Id" });
            DropIndex("dbo.CommentEntities", new[] { "CommentEntity_Id" });
            DropTable("dbo.PlatformTypeEntityGameEntities");
            DropTable("dbo.GenreEntityGameEntities");
            DropTable("dbo.PlatformTypeEntities");
            DropTable("dbo.GenreEntities");
            DropTable("dbo.GameEntities");
            DropTable("dbo.CommentEntities");
        }
    }
}
