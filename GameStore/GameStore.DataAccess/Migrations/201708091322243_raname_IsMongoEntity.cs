namespace GameStore.DataAccess.MSSQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class raname_IsMongoEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CommentEntities", "IsSqlEntity", c => c.Boolean(nullable: false));
            AddColumn("dbo.GameEntities", "IsSqlEntity", c => c.Boolean(nullable: false));
            AddColumn("dbo.GameInfoEntities", "IsSqlEntity", c => c.Boolean(nullable: false));
            AddColumn("dbo.GenreEntities", "IsSqlEntity", c => c.Boolean(nullable: false));
            AddColumn("dbo.PlatformTypeEntities", "IsSqlEntity", c => c.Boolean(nullable: false));
            AddColumn("dbo.PublisherEntities", "IsSqlEntity", c => c.Boolean(nullable: false));
            AddColumn("dbo.OrderDetailsEntities", "IsSqlEntity", c => c.Boolean(nullable: false));
            AddColumn("dbo.OrderEntities", "IsSqlEntity", c => c.Boolean(nullable: false));
            DropColumn("dbo.CommentEntities", "IsMongoEntity");
            DropColumn("dbo.GameEntities", "IsMongoEntity");
            DropColumn("dbo.GameInfoEntities", "IsMongoEntity");
            DropColumn("dbo.GenreEntities", "IsMongoEntity");
            DropColumn("dbo.PlatformTypeEntities", "IsMongoEntity");
            DropColumn("dbo.PublisherEntities", "IsMongoEntity");
            DropColumn("dbo.OrderDetailsEntities", "IsMongoEntity");
            DropColumn("dbo.OrderEntities", "IsMongoEntity");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrderEntities", "IsMongoEntity", c => c.Boolean(nullable: false));
            AddColumn("dbo.OrderDetailsEntities", "IsMongoEntity", c => c.Boolean(nullable: false));
            AddColumn("dbo.PublisherEntities", "IsMongoEntity", c => c.Boolean(nullable: false));
            AddColumn("dbo.PlatformTypeEntities", "IsMongoEntity", c => c.Boolean(nullable: false));
            AddColumn("dbo.GenreEntities", "IsMongoEntity", c => c.Boolean(nullable: false));
            AddColumn("dbo.GameInfoEntities", "IsMongoEntity", c => c.Boolean(nullable: false));
            AddColumn("dbo.GameEntities", "IsMongoEntity", c => c.Boolean(nullable: false));
            AddColumn("dbo.CommentEntities", "IsMongoEntity", c => c.Boolean(nullable: false));
            DropColumn("dbo.OrderEntities", "IsSqlEntity");
            DropColumn("dbo.OrderDetailsEntities", "IsSqlEntity");
            DropColumn("dbo.PublisherEntities", "IsSqlEntity");
            DropColumn("dbo.PlatformTypeEntities", "IsSqlEntity");
            DropColumn("dbo.GenreEntities", "IsSqlEntity");
            DropColumn("dbo.GameInfoEntities", "IsSqlEntity");
            DropColumn("dbo.GameEntities", "IsSqlEntity");
            DropColumn("dbo.CommentEntities", "IsSqlEntity");
        }
    }
}
