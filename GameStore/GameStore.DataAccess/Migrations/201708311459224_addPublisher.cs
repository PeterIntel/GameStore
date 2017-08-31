namespace GameStore.DataAccess.MSSQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addPublisher : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserEntities", "Publisher_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.UserEntities", "Publisher_Id");
            AddForeignKey("dbo.UserEntities", "Publisher_Id", "dbo.PublisherEntities", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserEntities", "Publisher_Id", "dbo.PublisherEntities");
            DropIndex("dbo.UserEntities", new[] { "Publisher_Id" });
            DropColumn("dbo.UserEntities", "Publisher_Id");
        }
    }
}
