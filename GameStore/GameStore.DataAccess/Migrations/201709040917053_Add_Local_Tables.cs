namespace GameStore.DataAccess.MSSQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Local_Tables : DbMigration
    {
        public override void Up()
        {
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
                        Name = c.String(),
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PublisherLocalEntities", "PublisherId", "dbo.PublisherEntities");
            DropForeignKey("dbo.PublisherLocalEntities", "CultureId", "dbo.CultureEntities");
            DropForeignKey("dbo.PlatformTypeLocalEntities", "PlatformTypeId", "dbo.PlatformTypeEntities");
            DropForeignKey("dbo.PlatformTypeLocalEntities", "CultureId", "dbo.CultureEntities");
            DropForeignKey("dbo.GameLocalEntities", "GameId", "dbo.GameEntities");
            DropForeignKey("dbo.GameLocalEntities", "CultureId", "dbo.CultureEntities");
            DropForeignKey("dbo.GenreLocalEntities", "GenreId", "dbo.GenreEntities");
            DropForeignKey("dbo.GenreLocalEntities", "CultureId", "dbo.CultureEntities");
            DropIndex("dbo.PublisherLocalEntities", new[] { "CultureId" });
            DropIndex("dbo.PublisherLocalEntities", new[] { "PublisherId" });
            DropIndex("dbo.PlatformTypeLocalEntities", new[] { "CultureId" });
            DropIndex("dbo.PlatformTypeLocalEntities", new[] { "PlatformTypeId" });
            DropIndex("dbo.GameLocalEntities", new[] { "CultureId" });
            DropIndex("dbo.GameLocalEntities", new[] { "GameId" });
            DropIndex("dbo.GenreLocalEntities", new[] { "CultureId" });
            DropIndex("dbo.GenreLocalEntities", new[] { "GenreId" });
            DropTable("dbo.PublisherLocalEntities");
            DropTable("dbo.PlatformTypeLocalEntities");
            DropTable("dbo.GameLocalEntities");
            DropTable("dbo.CultureEntities");
            DropTable("dbo.GenreLocalEntities");
        }
    }
}
