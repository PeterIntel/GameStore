namespace GameStore.DataAccess.MSSQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsDisabled_field_to_CommentEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CommentEntities", "IsDisabled", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CommentEntities", "IsDisabled");
        }
    }
}
