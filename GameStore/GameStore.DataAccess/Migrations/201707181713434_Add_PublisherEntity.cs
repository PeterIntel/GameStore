namespace GameStore.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_PublisherEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PublisherEntities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CompanyName = c.String(maxLength: 40),
                        Description = c.String(storeType: "ntext"),
                        HomePage = c.String(storeType: "ntext"),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.CompanyName, unique: true);
            
            AddColumn("dbo.GameEntities", "Publisher_Id", c => c.Int());
            CreateIndex("dbo.GameEntities", "Publisher_Id");
            AddForeignKey("dbo.GameEntities", "Publisher_Id", "dbo.PublisherEntities", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GameEntities", "Publisher_Id", "dbo.PublisherEntities");
            DropIndex("dbo.PublisherEntities", new[] { "CompanyName" });
            DropIndex("dbo.GameEntities", new[] { "Publisher_Id" });
            DropColumn("dbo.GameEntities", "Publisher_Id");
            DropTable("dbo.PublisherEntities");
        }
    }
}
