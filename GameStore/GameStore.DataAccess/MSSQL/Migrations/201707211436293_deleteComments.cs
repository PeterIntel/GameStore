namespace GameStore.DataAccess.MSSQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleteComments : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CommentEntities", "CommentEntity_Id", "dbo.CommentEntities");
            DropIndex("dbo.CommentEntities", new[] { "CommentEntity_Id" });
            DropColumn("dbo.CommentEntities", "CommentEntity_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CommentEntities", "CommentEntity_Id", c => c.Int());
            CreateIndex("dbo.CommentEntities", "CommentEntity_Id");
            AddForeignKey("dbo.CommentEntities", "CommentEntity_Id", "dbo.CommentEntities", "Id");
        }
    }
}
