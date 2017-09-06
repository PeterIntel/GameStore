namespace GameStore.DataAccess.MSSQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.GenreEntities", new[] { "GenreEntity_Id" });
            DropColumn("dbo.GenreEntities", "ParentGenreId");
            RenameColumn(table: "dbo.GenreEntities", name: "GenreEntity_Id", newName: "ParentGenreId");
            CreateTable(
                "dbo.GenreLocalEntities",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        GenreId = c.String(maxLength: 128),
                        Name = c.String(),
                        CultureId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CultureEntities", t => t.CultureId)
                .ForeignKey("dbo.GenreEntities", t => t.GenreId)
                .Index(t => t.GenreId)
                .Index(t => t.CultureId);
            
            CreateTable(
                "dbo.CultureEntities",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Title = c.String(),
                        Code = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        IsSqlEntity = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.GameLocalEntities",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        GameId = c.String(maxLength: 128),
                        Description = c.String(),
                        CultureId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CultureEntities", t => t.CultureId)
                .ForeignKey("dbo.GameEntities", t => t.GameId)
                .Index(t => t.GameId)
                .Index(t => t.CultureId);
            
            CreateTable(
                "dbo.PlatformTypeLocalEntities",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        PlatformTypeId = c.String(maxLength: 128),
                        TypeName = c.String(),
                        CultureId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CultureEntities", t => t.CultureId)
                .ForeignKey("dbo.PlatformTypeEntities", t => t.PlatformTypeId)
                .Index(t => t.PlatformTypeId)
                .Index(t => t.CultureId);
            
            CreateTable(
                "dbo.PublisherLocalEntities",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        PublisherId = c.String(maxLength: 128),
                        Description = c.String(),
                        CultureId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CultureEntities", t => t.CultureId)
                .ForeignKey("dbo.PublisherEntities", t => t.PublisherId)
                .Index(t => t.PublisherId)
                .Index(t => t.CultureId);
            
            CreateTable(
                "dbo.UserEntities",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Login = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        BirthDay = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        IsSqlEntity = c.Boolean(nullable: false),
                        Publisher_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PublisherEntities", t => t.Publisher_Id)
                .Index(t => t.Publisher_Id);
            
            CreateTable(
                "dbo.RoleEntities",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Role = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        IsSqlEntity = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RoleEntityUserEntities",
                c => new
                    {
                        RoleEntity_Id = c.String(nullable: false, maxLength: 128),
                        UserEntity_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.RoleEntity_Id, t.UserEntity_Id })
                .ForeignKey("dbo.RoleEntities", t => t.RoleEntity_Id, cascadeDelete: true)
                .ForeignKey("dbo.UserEntities", t => t.UserEntity_Id, cascadeDelete: true)
                .Index(t => t.RoleEntity_Id)
                .Index(t => t.UserEntity_Id);
            
            AddColumn("dbo.CommentEntities", "IsDisabled", c => c.Boolean(nullable: false));
            AlterColumn("dbo.GenreEntities", "ParentGenreId", c => c.String(maxLength: 128));
            AlterColumn("dbo.OrderEntities", "CustomerId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.GenreEntities", "ParentGenreId");
            CreateIndex("dbo.OrderEntities", "CustomerId");
            AddForeignKey("dbo.OrderEntities", "CustomerId", "dbo.UserEntities", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderEntities", "CustomerId", "dbo.UserEntities");
            DropForeignKey("dbo.RoleEntityUserEntities", "UserEntity_Id", "dbo.UserEntities");
            DropForeignKey("dbo.RoleEntityUserEntities", "RoleEntity_Id", "dbo.RoleEntities");
            DropForeignKey("dbo.UserEntities", "Publisher_Id", "dbo.PublisherEntities");
            DropForeignKey("dbo.PublisherLocalEntities", "PublisherId", "dbo.PublisherEntities");
            DropForeignKey("dbo.PublisherLocalEntities", "CultureId", "dbo.CultureEntities");
            DropForeignKey("dbo.PlatformTypeLocalEntities", "PlatformTypeId", "dbo.PlatformTypeEntities");
            DropForeignKey("dbo.PlatformTypeLocalEntities", "CultureId", "dbo.CultureEntities");
            DropForeignKey("dbo.GameLocalEntities", "GameId", "dbo.GameEntities");
            DropForeignKey("dbo.GameLocalEntities", "CultureId", "dbo.CultureEntities");
            DropForeignKey("dbo.GenreLocalEntities", "GenreId", "dbo.GenreEntities");
            DropForeignKey("dbo.GenreLocalEntities", "CultureId", "dbo.CultureEntities");
            DropIndex("dbo.RoleEntityUserEntities", new[] { "UserEntity_Id" });
            DropIndex("dbo.RoleEntityUserEntities", new[] { "RoleEntity_Id" });
            DropIndex("dbo.UserEntities", new[] { "Publisher_Id" });
            DropIndex("dbo.OrderEntities", new[] { "CustomerId" });
            DropIndex("dbo.PublisherLocalEntities", new[] { "CultureId" });
            DropIndex("dbo.PublisherLocalEntities", new[] { "PublisherId" });
            DropIndex("dbo.PlatformTypeLocalEntities", new[] { "CultureId" });
            DropIndex("dbo.PlatformTypeLocalEntities", new[] { "PlatformTypeId" });
            DropIndex("dbo.GameLocalEntities", new[] { "CultureId" });
            DropIndex("dbo.GameLocalEntities", new[] { "GameId" });
            DropIndex("dbo.GenreLocalEntities", new[] { "CultureId" });
            DropIndex("dbo.GenreLocalEntities", new[] { "GenreId" });
            DropIndex("dbo.GenreEntities", new[] { "ParentGenreId" });
            AlterColumn("dbo.OrderEntities", "CustomerId", c => c.String());
            AlterColumn("dbo.GenreEntities", "ParentGenreId", c => c.String());
            DropColumn("dbo.CommentEntities", "IsDisabled");
            DropTable("dbo.RoleEntityUserEntities");
            DropTable("dbo.RoleEntities");
            DropTable("dbo.UserEntities");
            DropTable("dbo.PublisherLocalEntities");
            DropTable("dbo.PlatformTypeLocalEntities");
            DropTable("dbo.GameLocalEntities");
            DropTable("dbo.CultureEntities");
            DropTable("dbo.GenreLocalEntities");
            RenameColumn(table: "dbo.GenreEntities", name: "ParentGenreId", newName: "GenreEntity_Id");
            AddColumn("dbo.GenreEntities", "ParentGenreId", c => c.String());
            CreateIndex("dbo.GenreEntities", "GenreEntity_Id");
        }
    }
}
