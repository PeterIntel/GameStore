namespace GameStore.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_GameInfo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GameInfoEntities",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        GameId = c.Int(nullable: false),
                        CountOfViews = c.Int(),
                        UploadDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GameEntities", t => t.Id)
                .Index(t => t.Id);
            
            AddColumn("dbo.GameEntities", "PublishedDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GameInfoEntities", "Id", "dbo.GameEntities");
            DropIndex("dbo.GameInfoEntities", new[] { "Id" });
            DropColumn("dbo.GameEntities", "PublishedDate");
            DropTable("dbo.GameInfoEntities");
        }
    }
}
